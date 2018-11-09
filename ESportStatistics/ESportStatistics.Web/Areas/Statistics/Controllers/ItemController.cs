using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Items;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IPDFService _pDFService;

        public ItemController(IItemService itemService, IPDFService pDFService)
        {
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
        }

        [HttpGet("items")]
        public async Task<IActionResult> Index(ItemViewModel item)
        {
            var items = await _itemService.FilterItemsAsync();

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

            var champions = await _itemService.FilterItemsAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (champions is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = champions.Select(c => new ItemDownloadViewModel(c));
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
