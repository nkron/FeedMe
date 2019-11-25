using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedMe.Models;
using FeedMe.Services;
using Microsoft.AspNetCore.Identity;
using FeedMe.Domains;
using Microsoft.AspNetCore.Authorization;

namespace FeedMe.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;

        public async Task<IActionResult> Index()
        {
            UserProfileViewModel model = new UserProfileViewModel();
            var user = await _userManager.GetUserAsync(User);
            model.FirstName = user.Username;
            model.LastName = user.LastName;
            model.Username = user.Username;
            model.Email = user.Email;

            return View(model);
        }

        public UserProfileController(UserService userService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
