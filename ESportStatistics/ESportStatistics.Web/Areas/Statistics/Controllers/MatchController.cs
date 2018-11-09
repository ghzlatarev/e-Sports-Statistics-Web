using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Matches;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            this._matchService = matchService;
        }

        [HttpGet]
        [Route("matches")]
        public async Task<IActionResult> Index(MatchViewModel match)
        {
            var matches = await _matchService.FilterMatchesAsync();

            var model = new MatchIndexViewModel(matches);

            return View(model);
        }

        [HttpGet]
        [Route("matches/filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var matches = await _matchService.FilterMatchesAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new MatchIndexViewModel(matches, searchTerm);

            return PartialView("_MatchTablePartial", model.Table);
        }

        [HttpGet]
        [Route("matches/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {

            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var match = await _matchService.FindAsync(id);

            if (match == null)
            {
                throw new ApplicationException($"Unable to find match with ID '{id}'.");
            }

            var model = new MatchDetailsViewModel(match);

            return View(model);
        }

    }
}
