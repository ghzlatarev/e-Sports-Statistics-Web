using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [Route("users")]
        public IActionResult Index()
        {
            var users = _userService.FilterUsers();

            var model = new IndexViewModel(users);

            return View(model);
        }

        [HttpGet]
        [Route("users-filter")]
        public IActionResult Filter(string searchTerm, int? pageSize, int? pageNumber)
        {
            var users = _userService.FilterUsers(
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(users, searchTerm);

            return PartialView("_UserTablePartial", model.Table);
        }
    }
}
