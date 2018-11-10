using ESportStatistics.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Administration.Controllers
{
    [Authorize]
    [Authorize(Roles = "Administrator")]
    public class RebaseController : Controller
    {
        private readonly IChampionService _championService;

        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;


        public RebaseController(IChampionService championService, IConfiguration configuration, ILogger logger)
        {
            _championService = championService ?? throw new ArgumentNullException(nameof(championService));

            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet("rebase/champions")]
        public async Task<IActionResult> Champions()
        {
            try
            {
                await _championService.RebaseChampionsAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated champions.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Champion repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }
    }
}
