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
using Microsoft.AspNetCore.Identity;

namespace FeedMe.Web.Controllers
{
    [Authorize]
    public class FoodController : Controller
    {
        private readonly FoodService _foodService;
        private readonly UserManager<User> _userManager;

        public FoodController(FoodService foodService, UserManager<User> userManager)
        {
            _foodService = foodService;
            _userManager = userManager;
        }

        public async Task<IActionResult> SearchFoodForMeal(int MealID,String date, int mealType)
        {
            HttpContext.Session.SetInt32("MealID", MealID);
            HttpContext.Session.SetString("MealDate", date);
            HttpContext.Session.SetInt32("MealType", mealType);

            FoodSearchViewModel VM = new FoodSearchViewModel();
            VM.Results = await ExecuteSearch(VM);
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
            foreach (Food f in results)
            {
                VM.Results.Add(new FoodViewModel(f));
            }
            VM.Date = HttpContext.Session.GetString("MealDate");
            return VM.Results;
        }

        public IActionResult AddToMeal(int foodID)
        {
            var user = _userManager.GetUserAsync(User);
            if (HttpContext.Session.GetInt32("MealID").Value == 0)
            {
                _foodService.AddFoodToNewMeal(HttpContext.Session.GetString("MealDate"), HttpContext.Session.GetInt32("MealType").Value, foodID, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }
            else
            {
                _foodService.AddFoodToExistingMeal(foodID, HttpContext.Session.GetInt32("MealID").Value);
            }
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
        public IActionResult SubmitFood(FoodDetailsViewModel vm)
        {
            if (HttpContext.Session.GetInt32("DetailsMealID").HasValue)
            {
                if(HttpContext.Session.GetInt32("DetailsMealID")!= HttpContext.Session.GetInt32("DetailsMealID"))
                {
                    CreateForMeal(vm);
                    return RedirectToAction("GoToDate", "MealPlanner", new { date = HttpContext.Session.GetString("DetailsMealDate") });
                }
                else
                {
                    UpdateForMeal(vm);
                }
            }
            else
            {
                CreateFood(vm);
            }


            var user = _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                //Create food, then add to referring meal and remove the original food from meal
                int foodID = _foodService.CreateFood(vm.FoodName, vm.FoodDesc, vm.Cals, vm.MacC, vm.MacF, vm.MacP, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                _foodService.AddFoodToExistingMeal(foodID,HttpContext.Session.GetInt32("DetailsMealID").Value);
                _foodService.RemoveFoodFromMeal(HttpContext.Session.GetInt32("DetailsMealID").Value, HttpContext.Session.GetInt32("DetailsFoodID").Value);                
                return RedirectToAction("GoToDate", "MealPlanner", new { date = HttpContext.Session.GetString("DetailsMealDate") });
            }
            //add validation errors
            return View("FoodDetails", vm);
        }

        private void UpdateForMeal(FoodDetailsViewModel vm)
        {
            throw new NotImplementedException();
        }

        

        private int CreateFood(FoodDetailsViewModel vm)
        {
            return _foodService.CreateFood(vm.FoodName, vm.FoodDesc, vm.Cals, vm.MacC, vm.MacF, vm.MacP, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        private void CreateForMeal(FoodDetailsViewModel vm)
        {
            int foodID = CreateFood(vm);
            _foodService.AddFoodToExistingMeal(foodID, HttpContext.Session.GetInt32("DetailsMealID").Value);
            _foodService.RemoveFoodFromMeal(HttpContext.Session.GetInt32("DetailsMealID").Value, HttpContext.Session.GetInt32("DetailsFoodID").Value);
        }
        [HttpPost]
        public IActionResult UpdateFood(FoodDetailsViewModel vm)
        {
            var user = _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                _foodService.UpdateFood(vm.FoodName, vm.FoodDesc, vm.Cals, vm.MacC, vm.MacF, vm.MacP, vm.FoodID);
            }
            ViewBag.message = vm.FoodName + " sucessfully saved!";
            ModelState.Clear();
            return View("NewFood", null);
        }

        public IActionResult ViewFoodDetails(int foodID, int mealID, string date)
        {
            HttpContext.Session.SetString("DetailsMealDate", date);
            HttpContext.Session.SetInt32("DetailsMealID", mealID);
            HttpContext.Session.SetInt32("DetailsFoodID", foodID);
            HttpContext.Session.SetInt32("DetailsFoodID", foodID);

            Food f = _foodService.getByID(Convert.ToInt32(foodID)); 
            FoodDetailsViewModel vm = new FoodDetailsViewModel(f);
            ViewBag.Mode = "view";
            ViewBag.UserID = _userManager.GetUserId(User);
            
            return View("FoodDetails", vm);
        }

            public IActionResult EditFoodDetails(string foodID, int mealID, string date)
        {
            Food f = _foodService.getByID(Convert.ToInt32(foodID));
            FoodDetailsViewModel vm = new FoodDetailsViewModel(f);
            ViewBag.Mode = "edit";
            ViewBag.UserID = _userManager.GetUserId(User);
            HttpContext.Session.SetString("DetailsMealDate", date);
            HttpContext.Session.SetInt32("DetailsMealID", mealID);

            return View("FoodDetails", vm);
        }
    }
}
