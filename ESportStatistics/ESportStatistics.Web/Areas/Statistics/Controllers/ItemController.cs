using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Items;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("items")]
        public async Task<IActionResult> Index(ItemViewModel item)
        {
            var items = await _itemService.FilterItemsAsync();

            var model = new ItemIndexViewModel(items);

            return View(model);
        }

        [HttpGet]
        [Route("items/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var items = await _itemService.FilterItemsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new ItemIndexViewModel(items, sortOrder, searchTerm);

            return PartialView("_ItemTablePartial", model.Table);
        }

        [HttpGet]
        [Route("items/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var item = await _itemService.FindAsync(id);
            if (item == null)
            {
                throw new ApplicationException($"Unable to find item with ID '{id}'.");
            }

            var model = new ItemDetailsViewModel(item);

            return View(model);
        }
    }
}
