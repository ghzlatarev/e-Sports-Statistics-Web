using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Teams;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Route("teams")]
    [Area("Statistics")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
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
