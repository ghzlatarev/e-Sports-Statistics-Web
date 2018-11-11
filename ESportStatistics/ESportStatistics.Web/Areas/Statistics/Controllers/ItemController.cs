using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Models;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using X.PagedList;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IPDFService _pDFService;
        private readonly IMemoryCache _memoryCache;

        public ItemController(IItemService itemService, IPDFService pDFService, IMemoryCache memoryCache)
        {
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("items")]
        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue("ListOfItems", out IPagedList<Item> items))
            {
                items = await _itemService.FilterItemsAsync();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(25),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                };

                _memoryCache.Set("ListOfItems", items, options);
            }

            var model = new ItemIndexViewModel(items);

            return View(model);
        }

        [HttpGet("items/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var items = await _itemService.FilterItemsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new ItemIndexViewModel(items, sortOrder, searchTerm);

            return PartialView("_ItemTablePartial", model.Table);
        }

        [HttpGet("items/details/{id}")]
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

        [HttpGet("items/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(ItemDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var items = await _itemService.FilterItemsAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (items is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = items.Select(c => new ItemDownloadViewModel(c));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "items");
            var fileBytes = await _pDFService.GetFileBytesAsync(outputFileName);

            try
            {
                return File(fileBytes, MediaTypeNames.Application.Octet, outputFileName);
            }
            finally
            {
                _pDFService.DeleteFile(outputFileName);
            }
        }
    }
}
