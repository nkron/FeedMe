using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedMe.Models;
using FeedMe.Services;

namespace FeedMe.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserService _userService;

        public IActionResult Index()
        {
            UserProfileViewModel model = new UserProfileViewModel();
            var user = _userService.getByID(1);
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Username = user.Username;
            model.Email = user.Email;

            return View(model);
        }

        public UserProfileController(UserService userService)
        {
            _userService = userService;
        }
        public ActionResult DoSomething()
        {
            UserProfileViewModel model = new UserProfileViewModel();
            model.FirstName = _userService.getByID(1).LastName;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
