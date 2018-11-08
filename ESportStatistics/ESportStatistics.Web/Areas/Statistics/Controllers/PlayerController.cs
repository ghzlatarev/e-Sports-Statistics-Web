using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Players;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Route("players")]
    [Area("Statistics")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var players = await _playerService.FilterPlayersAsync();

            var model = new IndexViewModel(players);

            return View(model);
        }

        [HttpGet]
        [Route("/players-filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var players = await _playerService.FilterPlayersAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(players, searchTerm);

            return PartialView("_PlayerTablePartial", model.Table);
        }
    }
}
