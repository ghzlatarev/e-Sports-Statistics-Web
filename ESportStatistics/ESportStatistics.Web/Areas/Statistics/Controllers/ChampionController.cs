using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Champions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class ChampionController : Controller
    {
        private readonly IChampionService _championService;
        private readonly IPDFService _pDFService;
        private readonly IMemoryCache _memoryCache;

        public ChampionController(IChampionService championService, IPDFService pDFService, IMemoryCache memoryCache)
        {
            _championService = championService ?? throw new ArgumentNullException(nameof(championService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("champions")]
        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue("ListOfChampions", out IPagedList<Champion> champions))
            {
                champions = await _championService.FilterChampionsAsync();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(25),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                };

                _memoryCache.Set("ListOfChampions", champions, options);
            }

            var model = new ChampionIndexViewModel(champions);

            return View(model);
        }

        [HttpGet("champions/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageNumber, int? pageSize)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var champions = await _championService.FilterChampionsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new ChampionIndexViewModel(champions, sortOrder, searchTerm);

            return PartialView("_ChampionTablePartial", model.Table);
        }

        [HttpGet("champions/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var champion = await _championService.FindAsync(id);
            if (champion == null)
            {
                throw new ApplicationException($"Unable to find champion with ID '{id}'.");
            }

            var model = new ChampionDetailsViewModel(champion);

            return View(model);
        }

        [HttpGet("champions/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(ChampionDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var champions = await _championService.FilterChampionsAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (champions is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = champions.Select(c => new ChampionDownloadViewModel(c));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "champions");
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
