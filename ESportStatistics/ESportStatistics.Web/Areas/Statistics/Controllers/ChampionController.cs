using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Champions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class ChampionController : Controller
    {
        private readonly IChampionService _championService;
        private readonly IPDFService _pDFService;

        public ChampionController(IChampionService championService, IPDFService pDFService)
        {
            _championService = championService ?? throw new ArgumentNullException(nameof(championService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
        }

        [HttpGet]
        [Route("champions")]
        public async Task<IActionResult> Index()
        {
            var champions = await _championService.FilterChampionsAsync();

            var model = new ChampionIndexViewModel(champions);

            return View(model);
        }

        [HttpGet]
        [Route("champions/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var champions = await _championService.FilterChampionsAsync(
                sortOrder,
                searchTerm,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new ChampionIndexViewModel(champions, sortOrder, searchTerm);

            return PartialView("_ChampionTablePartial", model.Table);
        }

        [HttpGet]
        [Route("champions/details/{id}")]
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
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var champions = await _championService.FilterChampionsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = champions.Select(c => new ChampionViewModel(c));

            IList<string> fileParameters = typeof(ChampionViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();
            const string fileName = "champions";

            string outputFileName = this._pDFService.CreatePDF(model, fileParameters, fileName);

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(outputFileName);

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
