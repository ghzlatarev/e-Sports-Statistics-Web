using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Series;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Route("series")]
    [Area("Statistics")]
    public class SerieController : Controller
    {
        private readonly ISerieService _serieService;

        public SerieController(ISerieService serieService)
        {
            _serieService = serieService ?? throw new ArgumentNullException(nameof(serieService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var spells = await _serieService.FilterSeriesAsync();

            var model = new IndexViewModel(spells);

            return View(model);
        }

        [HttpGet]
        [Route("/series-filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var spells = await _serieService.FilterSeriesAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(spells, searchTerm);

            return PartialView("_SerieTablePartial", model.Table);
        }
    }
}
