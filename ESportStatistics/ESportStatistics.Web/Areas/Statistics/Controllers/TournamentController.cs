using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Tournaments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException(nameof(tournamentService));
        }

        [HttpGet]
        [Route("tournaments")]
        public async Task<IActionResult> Index()
        {
            var tournaments = await _tournamentService.FilterTournamentsAsync();

            var model = new TournamentIndexViewModel(tournaments);

            return View(model);
        }

        [HttpGet]
        [Route("/tournaments/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var tournaments = await _tournamentService.FilterTournamentsAsync(
                sortOrder,
                searchTerm,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new TournamentIndexViewModel(tournaments, sortOrder, searchTerm);

            return PartialView("_TournamentTablePartial", model.Table);
        }

        [HttpGet]
        [Route("tournaments/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var tournament = await _tournamentService.FindAsync(id);
            if (tournament == null)
            {
                throw new ApplicationException($"Unable to find tournament with ID '{id}'.");
            }

            var model = new TournamentDetailsViewModel(tournament);

            return View(model);
        }
    }
}
