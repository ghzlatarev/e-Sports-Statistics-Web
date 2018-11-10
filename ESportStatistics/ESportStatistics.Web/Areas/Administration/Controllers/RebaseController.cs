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
        private readonly IItemService _itemService;
        private readonly ILeagueService _leagueService;
        private readonly IMasteryService _masteryService;
        private readonly IMatchService _matchService;
        private readonly IPlayerService _playerService;
        private readonly ISerieService _serieService;
        private readonly ISpellService _spellService;
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;

        private readonly IConfiguration _configuration;
        private readonly ILogger<RebaseController> _logger;


        public RebaseController(IChampionService championService, IItemService itemService, 
            ILeagueService leagueService, IMasteryService masteryService, IMatchService matchService,
            IPlayerService playerService, ITeamService teamService, ITournamentService tournamentService,
            ISpellService spellService, ISerieService serieService, IConfiguration configuration, 
            ILogger<RebaseController> logger)
        {
            _championService = championService ?? throw new ArgumentNullException(nameof(championService));
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            _leagueService = leagueService ?? throw new ArgumentNullException(nameof(leagueService));
            _masteryService = masteryService ?? throw new ArgumentNullException(nameof(masteryService));
            _matchService = matchService ?? throw new ArgumentNullException(nameof(matchService));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            _serieService = serieService ?? throw new ArgumentNullException(nameof(serieService));
            _spellService = spellService ?? throw new ArgumentNullException(nameof(spellService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _tournamentService = tournamentService ?? throw new ArgumentNullException(nameof(tournamentService));

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

        [HttpGet("rebase/items")]
        public async Task<IActionResult> Items()
        {
            try
            {
                await _itemService.RebaseItemsAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated items.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Items repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }

        [HttpGet("rebase/leagues")]
        public async Task<IActionResult> Leagues()
        {
            try
            {
                await _leagueService.RebaseLeaguesAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated leagues.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Leagues repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }

        [HttpGet("rebase/masteries")]
        public async Task<IActionResult> Masteries()
        {
            try
            {
                await _masteryService.RebaseMasteriesAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated masteries.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Masteries repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }

        [HttpGet("rebase/matches")]
        public async Task<IActionResult> Matches()
        {
            try
            {
                await _matchService.RebaseMatchesAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated matches.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Matches repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }

        [HttpGet("rebase/players")]
        public async Task<IActionResult> Players()
        {
            try
            {
                await _playerService.RebasePlayersAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated players.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Players repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }

        [HttpGet("rebase/series")]
        public async Task<IActionResult> Series()
        {
            try
            {
                await _serieService.RebaseSeriesAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated series.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Series repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }

        [HttpGet("rebase/spells")]
        public async Task<IActionResult> Spells()
        {
            try
            {
                await _spellService.RebaseSpellsAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated spells.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Spells repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }

        [HttpGet("rebase/teams")]
        public async Task<IActionResult> Teams()
        {
            try
            {
                await _teamService.RebaseTeamsAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated teams.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Teams repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }

        [HttpGet("rebase/tournaments")]
        public async Task<IActionResult> Tournaments()
        {
            try
            {
                await _tournamentService.RebaseTournamentsAsync(_configuration["PandaScoreAPIAccessToken"]);
                StatusMessage = "Successfully repopulated tournaments.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Tournaments repopulation unsuccessful!";
                _logger.LogError(ex.Message);
            }

            return PartialView("_StatusMessage", StatusMessage);
        }
    }
}
