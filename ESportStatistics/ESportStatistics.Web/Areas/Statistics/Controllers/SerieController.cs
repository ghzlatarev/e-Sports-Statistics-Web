using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using ESportStatistics.Web.Areas.Statistics.Models.Series;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    [Route("series")]
    public class SerieController : Controller
    {
        private readonly ILogger _logger;
        private readonly ISerieService _serieService;

        public SerieController(ILogger<AccountController> logger, ISerieService serieService)
        {
            _logger = logger;
            _serieService = serieService;
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
