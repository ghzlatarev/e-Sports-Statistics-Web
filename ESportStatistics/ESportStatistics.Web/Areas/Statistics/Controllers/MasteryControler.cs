using ESportStatistics.Web.Areas.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESportStatistics.Web.Areas.Statistics.Controllers
{
    [Authorize]
    [Area("Statistics")]
    [Authorize(Roles = "User")]
    [Route("[controller]/[action]")]
    public class MasteryController : Controller
    {
        
        private readonly ILogger _logger;

        public MasteryController(ILogger<AccountController> logger)
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
