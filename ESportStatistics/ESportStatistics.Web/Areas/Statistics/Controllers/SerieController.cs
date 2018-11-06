using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Authorize]
    [Area("Statistics")]
    [Authorize(Roles = "User")]
    [Route("[controller]/[action]")]
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
            var series = await _serieService.FilterSeriesAsync();

            var model = series.Select(s => new SerieViewModel(s));

            return View(model);
        }
    }
}
