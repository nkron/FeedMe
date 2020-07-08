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
using FeedMe.Domains.Enumerations;
using FeedMe.Web.Enumerations;

namespace FeedMe.Web.Controllers
{
    [Authorize]
    public class FoodController : Controller
    {
        private readonly FoodService _foodService;
        private readonly UserManager<User> _userManager;
        //Break this down into search and details controllers
        public FoodController(FoodService foodService, UserManager<User> userManager)
        {
            _foodService = foodService;
            _userManager = userManager;
        }

        public async Task<IActionResult> SearchFoodForMeal(int MealID, String date, int mealType)
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
            var v = _foodService.SearchAPI();
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
        public IActionResult FoodDetails(int foodID)
        {
            Food f = _foodService.getByID(Convert.ToInt32(foodID));
            FoodDetailsViewModel vm = new FoodDetailsViewModel(f);


            if (vm.CreatorID.ToString().Equals(_userManager.GetUserId(User)))
            {
                vm.SubmitType = (SubmitType)2;
            }
            else
            {
                vm.SubmitType = (SubmitType)1;
            }

            ViewBag.UserID = _userManager.GetUserId(User);

            return View("FoodDetails", vm);
        }
        public IActionResult FoodDetailsForMeal(int foodID, int mealID, string date)
        {
            HttpContext.Session.SetString("ReferringMealDate", date);
            HttpContext.Session.SetInt32("ReferringMealID", mealID);
            HttpContext.Session.SetInt32("ReferringFoodID", foodID);

            Food f = _foodService.getByID(Convert.ToInt32(foodID));
            FoodDetailsViewModel vm = new FoodDetailsViewModel(f);


            if (vm.CreatorID.ToString().Equals(_userManager.GetUserId(User)))
            {
                vm.SubmitType = (SubmitType)4;
            }
            else
            {
                vm.SubmitType = (SubmitType)3;
            }

            ViewBag.UserID = _userManager.GetUserId(User);

            return View("FoodDetails", vm);
        }       
        public IActionResult FoodDetailsViewOnly(int foodID)
        {
            Food f = _foodService.getByID(Convert.ToInt32(foodID));
            FoodDetailsViewModel vm = new FoodDetailsViewModel(f);

            vm.SubmitType = (SubmitType)0;

            ViewBag.UserID = _userManager.GetUserId(User);

            return View("FoodDetails", vm);
        }
        [HttpPost]
        public IActionResult SubmitFood(FoodDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                int caseSwitch = (int)vm.SubmitType;
                switch (caseSwitch)
                {
                    //Create
                    case 1:
                        CreateFood(vm);
                        return View("FoodDetails", vm);
                    //Update
                    case 2:
                        UpdateFood(vm);
                        return View("FoodDetails", vm);
                    //CreateForMeal
                    case 3:
                        CreateForMeal(vm);
                        return RedirectToAction("GoToDate", "MealPlanner", new { date = HttpContext.Session.GetString("ReferringMealDate") });
                    //UpdateForMeal
                    case 4:
                        UpdateFood(vm);
                        return RedirectToAction("GoToDate", "MealPlanner", new { date = HttpContext.Session.GetString("ReferringMealDate") });
                    default:
                        return View("FoodDetails", vm);
                }
            }
            else
            {
                //Add validation errors
                return View("FoodDetails", vm);
            }

        }

        private void UpdateFood(FoodDetailsViewModel vm)
        {
            _foodService.UpdateFood(vm.FoodName, vm.FoodDesc, vm.Cals, vm.MacC, vm.MacF, vm.MacP, vm.FoodID);
        }

        private int CreateFood(FoodDetailsViewModel vm)
        {
            return _foodService.CreateFood(vm.FoodName, vm.FoodDesc, vm.Cals, vm.MacC, vm.MacF, vm.MacP, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        private void CreateForMeal(FoodDetailsViewModel vm)
        {
            int foodID = CreateFood(vm);
            _foodService.AddFoodToExistingMeal(foodID, HttpContext.Session.GetInt32("ReferringMealID").Value);
            _foodService.RemoveFoodFromMeal(HttpContext.Session.GetInt32("ReferringMealID").Value, HttpContext.Session.GetInt32("ReferringMealID").Value);
        }

        //private IActionResult UpdateFood(FoodDetailsViewModel vm)
        //{
        //    var user = _userManager.GetUserAsync(User);
        //    if (ModelState.IsValid)
        //    {
        //        _foodService.UpdateFood(vm.FoodName, vm.FoodDesc, vm.Cals, vm.MacC, vm.MacF, vm.MacP, vm.FoodID);
        //    }
        //    ViewBag.message = vm.FoodName + " sucessfully saved!";
        //    ModelState.Clear();
        //    return View("NewFood", null);
        //}
    }
}
