using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Masteries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class MasteryController : Controller
    {
        private readonly IMasteryService _masteryService;
        private readonly IPDFService _pDFService;

        public MasteryController(IMasteryService masteryService, IPDFService pDFService)
        {
            _masteryService = masteryService ?? throw new ArgumentNullException(nameof(masteryService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
        }

        [HttpGet("masteries")]
        public async Task<IActionResult> Index(MasteryViewModel mastery)
        {
            var masteries = await _masteryService.FilterMasteriesAsync();

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

            var champions = await _masteryService.FilterMasteriesAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (champions is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = champions.Select(c => new MasteryDownloadViewModel(c));
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
