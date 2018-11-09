using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Champions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class ChampionController : Controller
    {
        private readonly IChampionService _championService;

        public ChampionController(IChampionService championService)
        {
            _championService = championService ?? throw new ArgumentNullException(nameof(championService));
        }

        [HttpGet]
        [Route("champions")]
        public async Task<IActionResult> Index()
        {
            var champions = await _championService.FilterChampionsAsync();

            var model = new ChampionIndexViewModel(champions);

            return View(model);
        }

        [HttpGet]
        [Route("champions/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var champions = await _championService.FilterChampionsAsync(
                sortOrder,
                searchTerm,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new ChampionIndexViewModel(champions, sortOrder, searchTerm);

            return PartialView("_ChampionTablePartial", model.Table);
        }

        [HttpGet]
        [Route("champions/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var champion = await _championService.FindAsync(id);
            if (champion == null)
            {
                throw new ApplicationException($"Unable to find champion with ID '{id}'.");
            }

            var model = new ChampionDetailsViewModel(champion);

            return View(model);
        }

    }
}
