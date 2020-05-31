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
using System.Security.Claims;

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

            FoodSearchViewModel VM = new FoodSearchViewModel();
            VM.Results = await ExecuteSearch(new FoodSearchViewModel());
            VM.Date = date;
            VM.MealType = 0;
             

            return View("SearchFood", VM);
        }

        public async Task<IActionResult> Search(FoodSearchViewModel VM)
        {
            VM.Results = await ExecuteSearch(VM);
            return View("SearchFood", VM);
        }
        public async Task<IActionResult> SearchClear(FoodSearchViewModel VM)
        {            
            VM.SearchName = null;
            VM.MealType = 0;
            VM.Results = await ExecuteSearch(VM);
            ModelState.Clear();
            return View("SearchFood", VM);
        }

        private async Task<List<FoodViewModel>> ExecuteSearch(FoodSearchViewModel VM)
        {
            VM.Results.Clear();
            List<Food> results = (List<Food>)await _foodService.Search(VM.SearchName, VM.MealType, VM.CalsMin, VM.CalsMax);
            foreach(Food f in results)
            {
                VM.Results.Add(new FoodViewModel(f));
            }
            VM.Date = HttpContext.Session.GetString("MealDate");
            return VM.Results;
        }
        public IActionResult AddToMeal(int foodID)
        {
            _foodService.AddFoodToMeal(HttpContext.Session.GetString("MealDate"), HttpContext.Session.GetInt32("MealType").Value,foodID, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            var s = HttpContext.Session.GetString("MealDate");
            return RedirectToAction("GoToDate", "MealPlanner", new { date = HttpContext.Session.GetString("MealDate") });
        }
        public IActionResult NewFood()
        {
            return View("NewFood");
        }
        public IActionResult BrowseFood()
        {
            return View("BrowseFood");
        }
        
        [HttpPost]
        public IActionResult SaveFood(FoodViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _foodService.UpdateFood(vm.FoodName, vm.Cals, vm.MacC, vm.MacF, vm.MacP, null);
            }
            ViewBag.message = vm.FoodName + " sucessfully saved!";
            ModelState.Clear();
            return View("NewFood", null);
        }
    }
}
