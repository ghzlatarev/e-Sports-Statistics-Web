using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Authorize]
    [Area("Statistics")]
    [Authorize(Roles = "User")]
    [Route("[controller]/[action]")]
    public class ChampionController : Controller
    {
        
        private readonly ILogger _logger;
        private readonly IChampionService _championService;


        public ChampionController(ILogger<AccountController> logger, IChampionService championService)
        {
            _logger = logger;
            _championService = championService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var champions = await _championService.FilterChampionsAsync();

            var model = champions.Select(c => new ChampionViewModel(c));
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var result = await _championService.ReturnChampionAsync(Id);

            var model = result.Select(c => new ChampionDetailsViewModel(c)).FirstOrDefault();

            return View(model);
        }

    }
}
