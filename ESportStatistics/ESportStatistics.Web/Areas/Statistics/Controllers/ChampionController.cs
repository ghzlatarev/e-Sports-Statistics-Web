using ESportStatistics.Core.Providers.Contracts;
using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Web.Areas.Identity.Controllers;
using ESportStatistics.Web.Areas.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Authorize]
    [Area("Statistics")]
    [Authorize(Roles = "User")]
    [Route("[controller]/[action]")]
    public class ChampionController : Controller
    {
        
        private readonly ILogger _logger;

        public ChampionController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

         [HttpGet]
        public IActionResult Index()
        {
            //var model = new ChampionViewModel
            //{
            //    //{
            //    //    PhoneNumber = user.PhoneNumber,
            //    //    ImageUrl = user.AvatarImageName,
            //    //    StatusMessage = StatusMessage
            //};
            return this.View();
        }

    }
}
