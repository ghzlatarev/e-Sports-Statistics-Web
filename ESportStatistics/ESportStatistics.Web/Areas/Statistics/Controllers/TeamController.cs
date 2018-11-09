using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Teams;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
        }

        [HttpGet]
        [Route("teams")]
        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.FilterTeamsAsync();

            var model = new TeamIndexViewModel(teams);

            return View(model);
        }

        [HttpGet]
        [Route("/teams/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var teams = await _teamService.FilterTeamsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new TeamIndexViewModel(teams, sortOrder, searchTerm);

            return PartialView("_TeamTablePartial", model.Table);
        }

        [HttpGet]
        [Route("teams/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var team = await _teamService.FindAsync(id);
            if (team == null)
            {
                throw new ApplicationException($"Unable to find team with ID '{id}'.");
            }

            var model = new TeamViewModel(team);

            return View(model);
        }
    }
}
