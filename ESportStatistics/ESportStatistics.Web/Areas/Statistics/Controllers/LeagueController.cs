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
    public class LeagueController : Controller
    {
        
        private readonly ILogger _logger;
        private readonly ILeagueService _leagueService;

        public LeagueController(ILogger<AccountController> logger, ILeagueService leagueService)
        {
            _logger = logger;
            _leagueService = leagueService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(LeagueViewModel league)
        {


            var leagues = await _leagueService.FilterLeaguesAsync();

            var model = leagues.Select(c => new LeagueViewModel(c));

            return View(model);
        }

    }
}
