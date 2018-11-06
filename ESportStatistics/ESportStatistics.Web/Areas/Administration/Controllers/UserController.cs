using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.FilterUsersAsync();

            var model = users.Select(u => new UserViewModel(u));

            return View(model);
        }

        [HttpGet]
        [Route("Administration/[controller]/[action]")]
        public async Task<IActionResult> Filter(string searchTerm = "")
        {
            if (searchTerm is null)
            {
                searchTerm = string.Empty;
            }

            var users = await _userService.FilterUsersAsync(searchTerm);

            var model = users.Select(u => new UserViewModel(u));

            return PartialView("_UserTablePartial", model);
        }
    }
}
