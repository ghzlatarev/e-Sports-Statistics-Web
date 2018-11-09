using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Players;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }

        [HttpGet]
        [Route("players")]
        public async Task<IActionResult> Index()
        {
            var players = await _playerService.FilterPlayersAsync();

            var model = new IndexViewModel(players);

            return View(model);
        }

        [HttpGet]
        [Route("/players/filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var players = await _playerService.FilterPlayersAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(players, searchTerm);

            return PartialView("_PlayerTablePartial", model.Table);
        }

        [HttpGet]
        [Route("players/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var player = await _playerService.FindAsync(id);
            if (player == null)
            {
                throw new ApplicationException($"Unable to find player with ID '{id}'.");
            }

            var model = new PlayerDetailsViewModel(player);

            return View(model);
        }
    }
}
