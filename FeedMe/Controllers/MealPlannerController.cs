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
    public class MealPlannerController : Controller
    {
        private readonly MealService _mealService;
        private readonly FoodService _foodService;
        private readonly UserManager<User> _userManager;

        public MealPlannerController(MealService mealService, FoodService foodService, UserManager<User> userManager)
        {
            _mealService = mealService;
            _foodService = foodService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            MealPlannerViewModel model = new MealPlannerViewModel(_mealService, _foodService, user, DateTime.Today);
            return View(model);
        }
        public IActionResult FirstTime()
        {           
            MealPlannerViewModel model = new MealPlannerViewModel();
            model.FirstTime = true;
            return View(model);
        }
        public async Task<IActionResult> GoToDate(string date)
        {
            var user = await _userManager.GetUserAsync(User);

            MealPlannerViewModel model = new MealPlannerViewModel(_mealService, _foodService, user, Convert.ToDateTime(date));
            return View("Index", model);
        }
        public async Task<IActionResult> NextDay(MealPlannerViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(model.Date).AddDays(1);
            model = new MealPlannerViewModel(_mealService, _foodService, user, dt);
            return View("Index", model);
        }
        public async Task<IActionResult> PreviousDay(MealPlannerViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(model.Date).AddDays(-1);
            model = new MealPlannerViewModel(_mealService, _foodService, user, dt);
            return View("Index", model);
        }
        public async Task<IActionResult> Today(MealPlannerViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(DateTime.Today);
            model = new MealPlannerViewModel(_mealService, _foodService, user, dt);
            return View("Index", model);
        }
        public async Task<IActionResult> RemoveMeal(MealPlannerViewModel model, int mealID)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(model.Date);
            _mealService.removeUserMeal(user.UserID, mealID, dt);

            model = new MealPlannerViewModel(_mealService, _foodService, user, dt);
            return View("Index", model);
        }
        public async Task<IActionResult> RemoveFoodFromMeal(string date, int mealID, int foodID, string APIFoodID)
        {
            var user = await _userManager.GetUserAsync(User);

            DateTime dt = Convert.ToDateTime(date);
            _foodService.RemoveFoodFromMeal(mealID, foodID, APIFoodID);
            MealPlannerViewModel model = new MealPlannerViewModel(_mealService, _foodService, user, dt);
            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
