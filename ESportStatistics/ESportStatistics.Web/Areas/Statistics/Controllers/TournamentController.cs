using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Tournaments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    [Authorize(Policy = "Default")]
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;
        private readonly IPDFService _pDFService;
        private readonly IMemoryCache _memoryCache;

        public TournamentController(ITournamentService tournamentService, IPDFService pDFService, IMemoryCache memoryCache)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException(nameof(tournamentService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("tournaments")]
        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue("ListOfTournaments", out IPagedList<Tournament> tournaments))
            {
                tournaments = await _tournamentService.FilterTournamentsAsync();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(25),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                };

                _memoryCache.Set("ListOfTournaments", tournaments, options);
            }

            var model = new TournamentIndexViewModel(tournaments);

            return View(model);
        }

        [HttpGet("/tournaments/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var tournaments = await _tournamentService.FilterTournamentsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new TournamentIndexViewModel(tournaments, sortOrder, searchTerm);

            return PartialView("_TournamentTablePartial", model.Table);
        }

        [HttpGet("tournaments/details/{id}")]
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

        [HttpGet("tournaments/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(TournamentDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var tournaments = await _tournamentService.FilterTournamentsAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (tournaments is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = tournaments.Select(t => new TournamentDownloadViewModel(t));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "tournaments");
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
