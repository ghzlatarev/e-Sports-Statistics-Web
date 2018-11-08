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
        [Route("users/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var user = await this._userService.FindAsync(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to find user with ID '{id}'.");
            }

            var model = new DetailsViewModel(user);

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

        [HttpGet]
        [Route("users/disable/{id}")]
        public async Task<IActionResult> Disable(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var user = await this._userService.DisableUser(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to find user with ID '{id}'.");
            }

            var model = new UserTableViewModel(user);

            return PartialView("_UserTableRowPartial", model);
        }

        [HttpGet]
        [Route("users/restore/{id}")]
        public async Task<IActionResult> Restore(string id)
        {
            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var user = await this._userService.RestoreUser(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to find user with ID '{id}'.");
            }

            var model = new UserTableViewModel(user);

            return PartialView("_UserTableRowPartial", model);
        }
    }
}
