using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Spells;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class SpellController : Controller
    {
        private readonly ISpellService _spellService;
        private readonly IPDFService _pDFService;

        public SpellController(ISpellService spellService, IPDFService pDFService)
        {
            _spellService = spellService ?? throw new ArgumentNullException(nameof(spellService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
        }

        [HttpGet("spells")]
        public async Task<IActionResult> Index()
        {
            var spells = await _spellService.FilterSpellsAsync();

            var model = new SpellIndexViewModel(spells);

            return View(model);
        }

        [HttpGet("/spells/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
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
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
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
