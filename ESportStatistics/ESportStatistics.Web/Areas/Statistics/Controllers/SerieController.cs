using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Series;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "Default")]
    public class SerieController : Controller
    {
        private readonly ISerieService _serieService;
        private readonly IPDFService _pDFService;
        private readonly IMemoryCache _memoryCache;

        public SerieController(ISerieService serieService, IPDFService pDFService, IMemoryCache memoryCache)
        {
            _serieService = serieService ?? throw new ArgumentNullException(nameof(serieService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("series")]
        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue("ListOfSeries", out IPagedList<Serie> series))
            {
                series = await _serieService.FilterSeriesAsync();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(25),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                };

                _memoryCache.Set("ListOfSeries", series, options);
            }

            var model = new SerieIndexViewModel(series);

            return View(model);
        }

        [HttpGet("/series/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var spells = await _serieService.FilterSeriesAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new SerieIndexViewModel(spells, sortOrder, searchTerm);

            return PartialView("_SerieTablePartial", model.Table);
        }

        [HttpGet("series/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var serie = await _serieService.FindAsync(id);
            if (serie == null)
            {
                throw new ApplicationException($"Unable to find serie with ID '{id}'.");
            }

            var model = new SerieDetailsViewModel(serie);

            return View(model);
        }

        [HttpGet("series/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(SerieDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var series = await _serieService.FilterSeriesAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (series is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = series.Select(s => new SerieDownloadViewModel(s));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "series");
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
