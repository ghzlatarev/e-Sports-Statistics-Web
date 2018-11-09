using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Leagues;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class LeagueController : Controller
    {
        
        private readonly ILeagueService _leagueService;

        public LeagueController( ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [HttpGet]
        [Route("leagues")]
        public async Task<IActionResult> Index(LeagueViewModel league)
        {
            var leagues = await _leagueService.FilterLeaguesAsync();

            var model = new LeagueIndexViewModel(leagues);

            return View(model);
        }

        [HttpGet]
        [Route("leagues/filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var leagues = await _leagueService.FilterLeaguesAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new LeagueIndexViewModel(leagues, searchTerm);

            return PartialView("_LeagueTablePartial", model.Table);
        }

        [HttpGet]
        [Route("leagues/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {

            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var league = await _leagueService.FindAsync(id);

            if (league == null)
            {
                throw new ApplicationException($"Unable to find league with ID '{id}'.");
            }

            var model = new LeagueDetailsViewModel(league);

            return View(model);
        }
    }
}
