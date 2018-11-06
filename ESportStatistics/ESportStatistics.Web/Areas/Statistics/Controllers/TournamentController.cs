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
    [Authorize(Roles = "User")]
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
        public async Task<IActionResult> Index(TournamentViewModel tournament)
        {
            var tournaments = await _tournamentService.FilterTournamentsAsync();

            var model = tournaments.Select(t => new TournamentViewModel(t));

            return View(model);
        }

    }
}
