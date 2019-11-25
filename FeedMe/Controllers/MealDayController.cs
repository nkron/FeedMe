using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedMe.Models;
using FeedMe.Services;
using Microsoft.AspNetCore.Authorization;

namespace FeedMe.Controllers
{
    [Authorize]
    public class MealDayController : Controller
    {
        private readonly MealService _mealService;
        private readonly FoodService _foodService;

        public IActionResult Index()
        {
            MealDayViewModel model = new MealDayViewModel(_mealService,_foodService, 1, DateTime.Today);
            return View(model);
        }

        public MealDayController(MealService mealService, FoodService foodService)
        {
            _mealService = mealService;
            _foodService = foodService;
        }
        public ActionResult DoSomething()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
