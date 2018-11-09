using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Authorize]
    [Area("Statistics")]
    [Authorize(Roles = "User")]
    [Route("[controller]/[action]")]
    public class ItemController : Controller
    {
        
        private readonly ILogger _logger;
        private readonly IItemService _itemService;

        public ItemController(ILogger<AccountController> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ItemViewModel item)
        {
            var name = item.Name;

            var items = await _itemService.FilterItemsAsync();

            var model = items.Select(c => new ItemViewModel(c));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var result = await _itemService.ReturnItemsAsync(Id);

            var model =  new ItemDetailsViewModel(result);

            return View(model);
        }

    }
}
