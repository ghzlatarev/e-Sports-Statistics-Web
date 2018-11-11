using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Masteries;
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
    public class MasteryController : Controller
    {
        private readonly IMasteryService _masteryService;
        private readonly IPDFService _pDFService;
        private readonly IMemoryCache _memoryCache;

        public MasteryController(IMasteryService masteryService, IPDFService pDFService, IMemoryCache memoryCache)
        {
            _masteryService = masteryService ?? throw new ArgumentNullException(nameof(masteryService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("masteries")]
        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue("ListOfMasteries", out IPagedList<Mastery> masteries))
            {
                masteries = await _masteryService.FilterMasteriesAsync();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(25),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                };

                _memoryCache.Set("ListOfMasteries", masteries, options);
            }

            var model = new MasteryIndexViewModel(masteries);

            return View(model);
        }

        [HttpGet("masteries/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var masteries = await _masteryService.FilterMasteriesAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new MasteryIndexViewModel(masteries, sortOrder, searchTerm);

            return PartialView("_MasteryTablePartial", model.Table);
        }

        [HttpGet("masteries/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(MasteryDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var masteries = await _masteryService.FilterMasteriesAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (masteries is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = masteries.Select(c => new MasteryDownloadViewModel(c));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "masteries");
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
