using FeedMe.Domains;
using FeedMe.Models;
using FeedMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FeedMe.Web.Controllers
{
    [Authorize]
    public class AddFoodController : Controller
    {
        private readonly FoodService _foodService;

        public AddFoodController(FoodService foodService)
        {
            _foodService = foodService;
        }

        public async Task<IActionResult> SearchFoodForMeal(String date, int mealType)
        {
            HttpContext.Session.SetString("MealDate", date);
            HttpContext.Session.SetInt32("MealType", mealType);
            FoodSearchViewModel VM = await ExecuteSearch(new FoodSearchViewModel());
            return View("SearchFood", VM);
        }

        public async Task<IActionResult> Search(FoodSearchViewModel VM)
        {
            VM = await ExecuteSearch(VM);
            return View("SearchFood", VM);
        }

        private async Task<FoodSearchViewModel> ExecuteSearch(FoodSearchViewModel VM)
        {
            VM.Results.Clear();
            List<Food> results = (List<Food>)await _foodService.Search(VM.SearchName, VM.MealType);
            foreach(Food f in results)
            {
                VM.Results.Add(new FoodViewModel(f));
            }
            VM.Date = HttpContext.Session.GetString("MealDate");
            return VM;
        }
        public async Task<IActionResult> AddToMeal(int foodID)
        {
            _foodService.AddFoodToMeal(HttpContext.Session.GetString("MealDate"), HttpContext.Session.GetString("MealType"),foodID);
            var s = HttpContext.Session.GetString("MealDate");
            return RedirectToAction("GoToDate", "MealDay", new { date = HttpContext.Session.GetString("MealDate") });
        }
    }
}
