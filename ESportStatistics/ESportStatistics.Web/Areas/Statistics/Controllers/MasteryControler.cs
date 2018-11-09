using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models.Masteries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class MasteryController : Controller
    {
        
        private readonly IMasteryService _masteryService;

        public MasteryController(ILogger<AccountController> logger, IMasteryService masteryService)
        {
            _masteryService = masteryService;
        }

        [HttpGet]
        [Route("masteries")]
        public async Task<IActionResult> Index(MasteryViewModel mastery)
        {

            var masteries = await _masteryService.FilterMasteriesAsync();

            var model = new MasteryIndexViewModel(masteries);

            return View(model);
        }

        [HttpGet]
        [Route("masteries/filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var masteries = await _masteryService.FilterMasteriesAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new MasteryIndexViewModel(masteries, searchTerm);

            return PartialView("_MasteryTablePartial", model.Table);
        }
    }
}
