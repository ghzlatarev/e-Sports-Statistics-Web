using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Series;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class SerieController : Controller
    {
        private readonly ISerieService _serieService;

        public SerieController(ISerieService serieService)
        {
            _serieService = serieService ?? throw new ArgumentNullException(nameof(serieService));
        }

        [HttpGet]
        [Route("series")]
        public async Task<IActionResult> Index()
        {
            var spells = await _serieService.FilterSeriesAsync();

            var model = new IndexViewModel(spells);

            return View(model);
        }

        [HttpGet]
        [Route("/series/filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var spells = await _serieService.FilterSeriesAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(spells, searchTerm);

            return PartialView("_SerieTablePartial", model.Table);
        }

        [HttpGet]
        [Route("series/details/{id}")]
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
    }
}
