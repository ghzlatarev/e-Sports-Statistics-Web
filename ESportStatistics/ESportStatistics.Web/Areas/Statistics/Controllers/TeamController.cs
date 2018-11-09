using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Web.Areas.Statistics.Models.Teams;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IPDFService _pDFService;

        public TeamController(ITeamService teamService, IPDFService pDFService)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _pDFService = pDFService ?? throw new ArgumentNullException(nameof(pDFService));
        }

        [HttpGet("teams")]
        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.FilterTeamsAsync();

            var model = new TeamIndexViewModel(teams);

            return View(model);
        }

        [HttpGet("/teams/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var teams = await _teamService.FilterTeamsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new TeamIndexViewModel(teams, sortOrder, searchTerm);

            return PartialView("_TeamTablePartial", model.Table);
        }

        [HttpGet("teams/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var team = await _teamService.FindAsync(id);
            if (team == null)
            {
                throw new ApplicationException($"Unable to find team with ID '{id}'.");
            }

            var model = new TeamViewModel(team);

            return View(model);
        }

        [HttpGet("teams/download")]
        public async Task<FileResult> Download(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            IList<string> fileParameters = typeof(TeamDownloadViewModel).GetProperties().Select(p => p.Name.ToString()).ToList();

            var teams = await _teamService.FilterTeamsAsync(sortOrder ?? string.Empty, searchTerm ?? string.Empty, pageNumber ?? 1, pageSize ?? 10);
            if (teams is null)
            {
                throw new ApplicationException("Failed to get database collection!");
            }

            var model = teams.Select(t => new TeamDownloadViewModel(t));
            var outputFileName = _pDFService.CreatePDF(model, fileParameters, "teams");
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
