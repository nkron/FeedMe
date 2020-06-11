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

        public async Task<IActionResult> Index()
        {            
            var user = await _userManager.GetUserAsync(User);
            UserProfileViewModel model = new UserProfileViewModel(user);

            return View(model);
        }

        public UserProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Update(UserProfileViewModel model)
        {
            if (ModelState.IsValid) {
                try { 
                await _userManager.UpdateAsync(new User { FirstName = model.FirstName, LastName = model.LastName, TargetCals=model.TargetCals,
                TargetMacC =model.TargetMacC,TargetMacF=model.TargetMacF,TargetMacP=model.TargetMacP, UserID= Convert.ToInt32(_userManager.GetUserId(User))});
                }
                catch(Exception e)
                {
                    throw new Exception(e.ToString());
                }
                ViewBag.Success = "Profile successfully updated!";
            }
            return View("index",model);
        }
    }
}
