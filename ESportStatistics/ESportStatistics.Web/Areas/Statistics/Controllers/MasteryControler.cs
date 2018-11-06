using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Authorize]
    [Area("Statistics")]
    [Authorize(Roles = "User")]
    [Route("[controller]/[action]")]
    public class MasteryController : Controller
    {
        
        private readonly ILogger _logger;
        private readonly IMasteryService _masteryService;

        public MasteryController(ILogger<AccountController> logger, IMasteryService masteryService)
        {
            _logger = logger;
            _masteryService = masteryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(MasteryViewModel mastery)
        {

            var masteries = await _masteryService.FilterMasteriesAsync();

            var model = masteries.Select(c => new MasteryViewModel(c));

            return View(model);
        }

    }
}
