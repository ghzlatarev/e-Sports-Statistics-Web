using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Spells;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class SpellController : Controller
    {
        private readonly ISpellService _spellService;
        private readonly IPDFService _pDFService;
        private readonly IMemoryCache _memoryCache;

        public SpellController(ISpellService spellService, IPDFService pDFService, IMemoryCache memoryCache)
        {
            _spellService = spellService ?? throw new ArgumentNullException(nameof(spellService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("spells")]
        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue("ListOfSpells", out IPagedList<Spell> spells))
            {
                spells = await _spellService.FilterSpellsAsync();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(25),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                };

                _memoryCache.Set("ListOfSpells", spells, options);
            }

            var model = new SpellIndexViewModel(spells);

            return View(model);
        }

        [HttpGet("/spells/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageNumber, int? pageSize)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var spells = await _spellService.FilterSpellsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new SpellIndexViewModel(spells, sortOrder, searchTerm);

            return PartialView("_SpellTablePartial", model.Table);
        }

        [HttpGet("spells/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var spell = await _spellService.FindAsync(id);
            if (spell == null)
            {
                throw new ApplicationException($"Unable to find spell with ID '{id}'.");
            }

            var model = new SpellDetailsViewModel(spell);

            return View(model);
        }

        [HttpGet("spells/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageNumber, int? pageSize)
        {
            IList<string> fileParameters = typeof(SpellDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var spells = await _spellService.FilterSpellsAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (spells is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = spells.Select(s => new SpellDownloadViewModel(s));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "spells");
            var fileBytes = await _pDFService.GetFileBytesAsync(outputFileName);

            try
            {
                return File(fileBytes, MediaTypeNames.Application.Octet, outputFileName);
            }
            finally
            {
                _pDFService.DeleteFile(outputFileName);
            }
        }
    }
}
