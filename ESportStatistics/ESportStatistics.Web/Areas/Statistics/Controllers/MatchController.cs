﻿using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Matches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    [Authorize(Policy = "Default")]
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly IPDFService _pDFService;

        public MatchController(IMatchService matchService, IPDFService pDFService)
        {
            this._matchService = matchService ?? throw new ArgumentNullException(nameof(matchService));
            this._pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
        }

        [HttpGet("matches")]
        public async Task<IActionResult> Index(MatchViewModel match)
        {
            var matches = await _matchService.FilterMatchesAsync();

            var model = new MatchIndexViewModel(matches);

            return View(model);
        }

        [HttpGet("matches/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var matches = await _matchService.FilterMatchesAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new MatchIndexViewModel(matches, sortOrder, searchTerm);

            return PartialView("_MatchTablePartial", model.Table);
        }
        
        [HttpGet("matches/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var match = await _matchService.FindAsync(id);
            if (match == null)
            {
                throw new ApplicationException($"Unable to find match with ID '{id}'.");
            }

            var model = new MatchDetailsViewModel(match);

            return View(model);
        }
        
        [HttpGet("matches/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(MatchDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var champions = await _matchService.FilterMatchesAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (champions is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = champions.Select(c => new MatchDownloadViewModel(c));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "matches");
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
