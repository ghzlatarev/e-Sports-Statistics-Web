using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using ESportStatistics.Web.Areas.Statistics.Models.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    [Route("teams")]
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

            var model = new IndexViewModel(teams);

            return View(model);
        }

        [HttpGet]
        [Route("/teams-filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var teams = await _teamService.FilterTeamsAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(teams, searchTerm);

            return PartialView("_TeamTablePartial", model.Table);
        }
    }
}
