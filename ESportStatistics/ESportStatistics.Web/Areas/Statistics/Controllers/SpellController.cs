using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using ESportStatistics.Web.Areas.Statistics.Models.Spells;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    [Route("spells")]
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

            var model = new IndexViewModel(spells);

            return View(model);
        }

        [HttpGet]
        [Route("/spells-filter")]
        public async Task<IActionResult> Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var spells = await _spellService.FilterSpellsAsync(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(spells, searchTerm);

            return PartialView("_SpellTablePartial", model.Table);
        }
    }
}
