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
using FeedMe.Repositories;

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
            //Using session instead of hidden VM vals because it can be used by multiple methods
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
            VM.Date = HttpContext.Session.GetString("MealDate");
            ModelState.Clear();
            return View("SearchFood", VM);
        }

        private async Task<List<FoodViewModel>> ExecuteSearch(FoodSearchViewModel VM)
        {
            VM.Results.Clear();
            List<FoodViewModel> results = new List<FoodViewModel>();

            List<Food> localFoods = (List<Food>)await _foodService.Search(VM.SearchName, VM.Brand, VM.CalsMin, VM.CalsMax, VM.MacCMin, VM.MacCMax, VM.MacPMin, VM.MacPMax, VM.MacFMin, VM.MacFMax);
            foreach (Food f in localFoods)
            {
                results.Add(new FoodViewModel(f));
            }

            List<FoodViewModel> APIFoods = ExecuteAPISearch(VM).Result;
            foreach (FoodViewModel f in APIFoods.ToList())
            {
                //Refine search by ranges because API doesn't offer feature
                if (f.Cals > VM.CalsMin && f.Cals < VM.CalsMax && f.MacC > VM.MacCMin && f.MacC < VM.MacCMax && f.MacP > VM.MacPMin && f.MacP < VM.MacPMax && f.MacP > VM.MacPMin && f.MacP < VM.MacPMax)
                    results.Add(f);
            }

            VM.Date = HttpContext.Session.GetString("MealDate");
            return results;
        }

        private async Task<List<FoodViewModel>> ExecuteAPISearch(FoodSearchViewModel VM)
        {
            VM.Results.Clear();
            List<Food> results = (List<Food>)await APIHelper.SearchFood(VM.SearchName);
            foreach (Food f in results)
            {
                VM.Results.Add(new FoodViewModel(f));
            }
            VM.Date = HttpContext.Session.GetString("SearchMealDate");
            return VM.Results;
        }


        //public IActionResult FoodDetails(int foodID)
        //{
        //    Food f = _foodService.getByID(Convert.ToInt32(foodID));
        //    FoodDetailsViewModel vm = new FoodDetailsViewModel(f);


        //    if (vm.CreatorID.ToString().Equals(_userManager.GetUserId(User)))
        //    {
        //        vm.SubmitType = (SubmitType)2;
        //    }
        //    else
        //    {
        //        vm.SubmitType = (SubmitType)1;
        //    }

        //    ViewBag.UserID = _userManager.GetUserId(User);

        //    return View("FoodDetails", vm);
        //}
        public IActionResult FoodDetailsForMealSearch(int foodID, string apiID, string imageURL)
        {
            return FoodDetails(foodID, HttpContext.Session.GetInt32("MealID").Value, HttpContext.Session.GetString("MealDate"), apiID, imageURL);
        }

        public IActionResult FoodDetailsForMeal(int foodID, int mealID, string date)
        {
            HttpContext.Session.SetString("MealDate", date);
            HttpContext.Session.SetInt32("MealID", mealID);
            HttpContext.Session.SetInt32("FoodID", foodID);
            return FoodDetails(foodID, mealID, date, null, null);
        }
        public IActionResult FoodDetails(int foodID, int mealID, string date, string apiID, string imageURL)
        {
            Food f;
            FoodDetailsViewModel vm;
            //Used to get API Food Details in future
            //if (foodID == 0)
            //{
            //    f = APIHelper.GetFoodByID(apiID).Result;
            //    vm = new FoodDetailsViewModel(f);
            //    vm.ImageURL = imageURL;
            //}
            f = _foodService.getByID(Convert.ToInt32(foodID));
            vm = new FoodDetailsViewModel(f);

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
        public IActionResult CreateCustomFood(int mealID, string date)
        {
            FoodDetailsViewModel vm = new FoodDetailsViewModel();

            vm.SubmitType = (SubmitType)1;
            ViewBag.Mode = "create";
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
                    //Create for meal
                    case 1:
                        CreateForMeal(vm);
                        return RedirectToAction("GoToDate", "MealPlanner", new { date = HttpContext.Session.GetString("MealDate") });
                    //Update (unused - will be for updating food unrelated to meal planner)
                    case 2:
                        UpdateFood(vm);
                        return View("FoodDetails", vm);
                    //Create for meal and replace old record
                    case 3:
                        CreateAndReplaceForMeal(vm);
                        return RedirectToAction("GoToDate", "MealPlanner", new { date = HttpContext.Session.GetString("MealDate") });
                    //Update For Meal
                    case 4:
                        UpdateFood(vm);
                        return RedirectToAction("GoToDate", "MealPlanner", new { date = HttpContext.Session.GetString("MealDate") });
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
            _foodService.UpdateFood(vm.FoodName, vm.Brand, vm.FoodDesc, vm.Cals, vm.MacC, vm.MacF, vm.MacP, vm.FoodID);
        }

        private int CreateFood(FoodDetailsViewModel vm)
        {
            return _foodService.CreateFood(vm.FoodName, vm.FoodDesc, vm.Brand, vm.Cals, vm.MacC, vm.MacF, vm.MacP, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)), vm.APIFoodID, vm.ImageURL);
        }
        private int CreateFood(FoodViewModel vm)
        {
            return _foodService.CreateFood(vm.FoodName, vm.FoodDesc, vm.Brand, vm.Cals, vm.MacC, vm.MacF, vm.MacP, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)), vm.APIFoodID, vm.ImageURL);
        }
        private int CreateFood(string FoodName, string FoodDesc, string brand, int Cals, int MacC, int MacF, int MacP, string APIFoodID, string ImageURL)
        {
            return _foodService.CreateFood(FoodName, FoodDesc, brand, Cals, MacC, MacF, MacP, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)), APIFoodID, ImageURL);
        }
        private void CreateForMeal(FoodDetailsViewModel vm)
        {
            int foodID = CreateFood(vm);
            if (HttpContext.Session.GetInt32("MealID").Value == 0)
            {
                _foodService.AddFoodToNewMeal(HttpContext.Session.GetString("MealDate"), HttpContext.Session.GetInt32("MealType").Value, foodID, null, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }
            else
            {
                _foodService.AddFoodToExistingMeal(foodID, HttpContext.Session.GetInt32("MealID").Value, null);
            }
        }
        private void CreateAndReplaceForMeal(FoodDetailsViewModel vm)
        {
            int foodID = CreateFood(vm);
            _foodService.AddFoodToExistingMeal(foodID, HttpContext.Session.GetInt32("MealID").Value, null);
            //meal id twice?
            _foodService.RemoveFoodFromMeal(HttpContext.Session.GetInt32("MealID").Value, HttpContext.Session.GetInt32("MealID").Value, HttpContext.Session.GetString("FoodID"));
        }
        public IActionResult AddToMeal(int FoodID, string APIFoodID, string brand, int Cals, int MacC, int MacF, int MacP, string FoodName, string ImageURL)
        {
            int foodID = FoodID;
            //Create API food in db if local version doesn't already exist
            if (APIFoodID != null)
            {
                int f = _foodService.GetAPIFoodInLocal(APIFoodID);
                if (f == 0)
                {
                    foodID = CreateFood(FoodName, null, brand, Cals, MacC, MacF, MacP, APIFoodID, ImageURL);
                }

                else
                {
                    foodID = f;
                }
            }
            var user = _userManager.GetUserAsync(User);
            if (HttpContext.Session.GetInt32("MealID").Value == 0)
            {
                _foodService.AddFoodToNewMeal(HttpContext.Session.GetString("MealDate"), HttpContext.Session.GetInt32("MealType").Value, foodID, APIFoodID, Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }
            else
            {
                _foodService.AddFoodToExistingMeal(foodID, HttpContext.Session.GetInt32("MealID").Value, APIFoodID);
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
