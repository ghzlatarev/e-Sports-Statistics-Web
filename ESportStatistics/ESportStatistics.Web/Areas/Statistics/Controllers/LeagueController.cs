using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Leagues;
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
    public class LeagueController : Controller
    {
        private readonly ILeagueService _leagueService;
        private readonly IPDFService _pDFService;
        private readonly IMemoryCache _memoryCache;

        public LeagueController(ILeagueService leagueService, IPDFService pDFService, IMemoryCache memoryCache)
        {
            _leagueService = leagueService ?? throw new ArgumentNullException(nameof(leagueService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("leagues")]
        public async Task<IActionResult> Index(LeagueViewModel league)
        {
            if (!_memoryCache.TryGetValue("ListOfILeagues", out IPagedList<League> leagues))
            {
                leagues = await _leagueService.FilterLeaguesAsync();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(25),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                };

                _memoryCache.Set("ListOfILeagues", leagues, options);
            }

            var model = new LeagueIndexViewModel(leagues);

            return View(model);
        }

        [HttpGet("leagues/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var leagues = await _leagueService.FilterLeaguesAsync(
                sortOrder,
                searchTerm,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new LeagueIndexViewModel(leagues, sortOrder, searchTerm);

            return PartialView("_LeagueTablePartial", model.Table);
        }

        [HttpGet("leagues/details/{id}")]
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

        [HttpGet("leagues/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(LeagueDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var champions = await _leagueService.FilterLeaguesAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (champions is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = champions.Select(c => new LeagueDownloadViewModel(c));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "leagues");
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
