using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Spells;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Route("spells")]
    [Area("Statistics")]
    public class SpellController : Controller
    {
        private readonly ISpellService _spellService;

        public SpellController(ISpellService spellService)
        {
            _spellService = spellService ?? throw new ArgumentNullException(nameof(spellService));
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
