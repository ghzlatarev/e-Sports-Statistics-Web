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
    //[Authorize(Roles = "User")]
    [Route("[controller]/[action]")]
    public class TeamController : Controller
    {
        private readonly ILogger _logger;
        private readonly ITeamService _teamService;
        
        public TeamController(ILogger<AccountController> logger, ITeamService teamService)
        {
            _logger = logger;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.FilterTeamsAsync();

            var model = teams.Select(t => new TeamViewModel(t));

            return View(model);
        }
    }
}
