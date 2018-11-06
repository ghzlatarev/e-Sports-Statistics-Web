using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Authorize]
    [Area("Statistics")]
    [Authorize(Roles = "User")]
    [Route("[controller]/[action]")]
    public class SpellController : Controller
    {
        private readonly ILogger _logger;
        private readonly ISpellService _spellService;

        public SpellController(ILogger<AccountController> logger, ISpellService spellService)
        {
            _logger = logger;
            _spellService = spellService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var spells = await _spellService.FilterSpellsAsync();

            var model = spells.Select(s => new SpellViewModel(s));

            return View(model);
        }
    }
}
