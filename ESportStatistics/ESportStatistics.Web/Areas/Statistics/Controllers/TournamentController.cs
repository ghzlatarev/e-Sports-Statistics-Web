using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using ESportStatistics.Web.Areas.Statistics.Models.Tournaments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    [Route("[controller]/[action]")]
    public class TournamentController : Controller
    {

        private readonly ILogger _logger;
        private readonly ITournamentService _tournamentService;


        public TournamentController(ILogger<AccountController> logger, ITournamentService tournamentService)
        {
            _logger = logger;
            _tournamentService = tournamentService;
        }

        [HttpGet]
        //[Route("tournaments")]
        public async Task<IActionResult> Index()
        {
            var tournaments = await _tournamentService.FilterTournamentsAsync();

            var model = new IndexViewModel(tournaments);

            return View(model);
        }

        [HttpGet]
        //[Route("users-filter")]
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
