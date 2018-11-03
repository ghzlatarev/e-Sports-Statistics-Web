using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            _userService = userService ?? throw ArgumentNullException(nameof(userService));
        }

        private Exception ArgumentNullException(object p)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.FilterUsersAsync();

            var model = new IndexViewModel(users);

            return View(model);
        }
    }
}
