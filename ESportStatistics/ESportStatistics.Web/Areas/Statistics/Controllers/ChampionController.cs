using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<IActionResult> Index(ChampionViewModel champion)
        {
            var name = champion.Name;

            var armor = champion.Armor;

            var champions = await _championService.FilterChampionsAsync();

            var model = champions.Select(c => new ChampionViewModel(c));
            
            return View(model);
        }

    }
}
