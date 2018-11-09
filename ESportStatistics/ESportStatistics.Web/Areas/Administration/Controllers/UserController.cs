using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.FilterUsersAsync();

            var model = new IndexViewModel(users);

            return View(model);
        }

        [HttpGet]
        [Route("users/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            const string adminRole = "Administrator";

            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to find user with ID '{id}'.");
            }

            var model = new DetailsViewModel(user, await _userManager.IsInRoleAsync(user, adminRole));

            return View(model);
        }

        [HttpGet]
        [Route("users/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            var users = await _userService.FilterUsersAsync(
                sortOrder ?? string.Empty,
                searchTerm ?? string.Empty,
                pageNumber ?? 1,
                pageSize ?? 10);

            var model = new IndexViewModel(users, sortOrder, searchTerm);

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

            var user = await _userService.DisableUser(id);
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

            var user = await _userService.RestoreUser(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to find user with ID '{id}'.");
            }

            var model = new UserTableViewModel(user);

            return PartialView("_UserTableRowPartial", model);
        }

        [HttpGet]
        [Route("users/promote/{id}")]
        public async Task<IActionResult> Promote(string id)
        {
            const string adminRole = "Administrator";

            if (!await _roleManager.RoleExistsAsync(adminRole))
            {
                throw new ApplicationException(string.Format("User promotion unsuccessful , {0} role does not exists.", adminRole));
            }

            var user = await _userManager.FindByIdAsync(id);

            var addRoleResult = await _userManager.AddToRoleAsync(user, adminRole);
            if (!addRoleResult.Succeeded)
            {
                throw new ApplicationException(string.Format("User promotion operation was unsuccessful."));
            }

            var model = new UserRoleViewModel(user, await _userManager.IsInRoleAsync(user, adminRole));

            return PartialView("_UserRolePartial", model);
        }

        [HttpGet]
        [Route("users/demote/{id}")]
        public async Task<IActionResult> Demote(string id)
        {
            const string adminRole = "Administrator";

            if (!await _roleManager.RoleExistsAsync(adminRole))
            {
                throw new ApplicationException(string.Format("User demotion unsuccessful , {0} role does not exists.", adminRole));
            }

            var user = await _userManager.FindByIdAsync(id);

            var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, adminRole);
            if (!removeRoleResult.Succeeded)
            {
                throw new ApplicationException(string.Format("User demotion operation was unsuccessful."));
            }

            var model = new UserRoleViewModel(user, await _userManager.IsInRoleAsync(user, adminRole));

            return PartialView("_UserRolePartial", model);
        }
    }
}
