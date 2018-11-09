using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Spells;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class SpellController : Controller
    {
        private readonly ISpellService _spellService;

        public SpellController(ISpellService spellService)
        {
            _spellService = spellService ?? throw new ArgumentNullException(nameof(spellService));
        }

        [HttpGet]
        [Route("spells")]
        public async Task<IActionResult> Index()
        {
            var spells = await _spellService.FilterSpellsAsync();

            var model = new SpellIndexViewModel(spells);

            return View(model);
        }

        [HttpGet]
        [Route("/spells/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var spells = await _spellService.FilterSpellsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new SpellIndexViewModel(spells, sortOrder, searchTerm);

            return PartialView("_SpellTablePartial", model.Table);
        }

        [HttpGet]
        [Route("spells/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var spell = await _spellService.FindAsync(id);
            if (spell == null)
            {
                throw new ApplicationException($"Unable to find spell with ID '{id}'.");
            }

            var model = new SpellViewModel(spell);

            return View(model);
        }
    }
}
