using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using ESportStatistics.Web.Areas.Statistics.Models.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    [Route("players")]
    public class PlayerController : Controller
    {
        private readonly ILogger _logger;
        private readonly IPlayerService _playerService;

        public PlayerController(ILogger<AccountController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
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
