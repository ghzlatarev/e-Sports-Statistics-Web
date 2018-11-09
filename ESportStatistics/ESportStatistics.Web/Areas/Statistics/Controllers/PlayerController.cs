using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Players;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IPDFService _pDFService;

        public PlayerController(IPlayerService playerService, IPDFService pDFService)
        {
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
        }

        [HttpGet("players")]
        public async Task<IActionResult> Index()
        {
            var players = await _playerService.FilterPlayersAsync();

            var model = new PlayerIndexViewModel(players);

            return View(model);
        }

        [HttpGet("/players/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var players = await _playerService.FilterPlayersAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new PlayerIndexViewModel(players, sortOrder, searchTerm);

            return PartialView("_PlayerTablePartial", model.Table);
        }

        [HttpGet("players/details/{id}")]
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

        [HttpGet("players/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(PlayerDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var players = await _playerService.FilterPlayersAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (players is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = players.Select(s => new PlayerDownloadViewModel(s));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "players");
            var fileBytes = await _pDFService.GetFileBytesAsync(outputFileName);

            try
            {
                return File(fileBytes, MediaTypeNames.Application.Octet, outputFileName);
            }
            finally
            {
                _pDFService.DeleteFile(outputFileName);
            }
        }
    }
}
