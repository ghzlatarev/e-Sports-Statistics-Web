using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Tournaments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Route("tournaments")]
    [Area("Statistics")]
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException(nameof(tournamentService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tournaments = await _tournamentService.FilterTournamentsAsync();

            var model = new IndexViewModel(tournaments);

            return View(model);
        }

        [HttpGet]
        [Route("/tournaments-filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var tournaments = await _tournamentService.FilterTournamentsAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(tournaments, searchTerm);

            return PartialView("_TournamentTablePartial", model.Table);
        }
    }
}
