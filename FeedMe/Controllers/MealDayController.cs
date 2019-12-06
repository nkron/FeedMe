using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedMe.Models;
using FeedMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FeedMe.Domains;

namespace FeedMe.Controllers
{
    [Authorize]
    public class MealDayController : Controller
    {
        private readonly MealService _mealService;
        private readonly FoodService _foodService;
        private readonly UserManager<User> _userManager;

        public MealDayController(MealService mealService, FoodService foodService, UserManager<User> userManager)
        {
            _mealService = mealService;
            _foodService = foodService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            MealDayViewModel model = new MealDayViewModel(_mealService,_foodService,user , DateTime.Today);
            return View(model);
        }
        public async Task<IActionResult> NextDay(MealDayViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(model.Date).AddDays(1);
            model = new MealDayViewModel(_mealService, _foodService, user, dt);
            return View("Index",model);
        }
        public async Task<IActionResult> PreviousDay(MealDayViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(model.Date).AddDays(-1);
            model = new MealDayViewModel(_mealService, _foodService, user, dt);
            return View("Index", model);
        }

        public async Task<IActionResult> DeleteMeal(MealDayViewModel model, int mealID)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(model.Date);
            _mealService.removeUserMeal(user.UserID, mealID, dt);

            model = new MealDayViewModel(_mealService, _foodService, user,dt);
            return View("Index", model);
        }
        public async Task<IActionResult> DeleteMealFood(MealDayViewModel model, int mealID)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(model.Date);
            _mealService.removeUserMeal(user.UserID, mealID, dt);

            model = new MealDayViewModel(_mealService, _foodService, user, dt);
            return View("Index", model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
