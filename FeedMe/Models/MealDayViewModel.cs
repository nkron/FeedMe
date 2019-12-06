using System;
using System.ComponentModel.DataAnnotations;
using FeedMe.Services;
using FeedMe.Domains;
using System.Collections.Generic;

namespace FeedMe.Models
{
    public class MealDayViewModel
    {
        MealService _mealService;
        MealService _foodService;

        //The date on the page - used to get meals for this day
        [Display(Name = "Date:")]
        public int totalCals { get; set; }

        public int totalMacC { get; set; }
        public int totalMacF { get; set; }
        public int totalMacP { get; set; }
        public int targetCals { get; set; }
        public int targetMacC { get; set; }
        public int targetMacP { get; set; }
        public int targetMacF { get; set; }
        public int targetMacCTotal { get; set; }
        public int targetMacPTotal { get; set; }
        public int targetMacFTotal { get; set; }
        public String Date{get; set;}

        public List<MealDetailsViewModel> meals { get; set; }

        public MealDayViewModel()
        {

        }
        public MealDayViewModel(MealService mealService, FoodService _foodService, User user, DateTime d)
        {
            _mealService = mealService;
            
            //Put each meal in a mealviewmodel
            var m = _mealService.getUserMealsByDate(user.UserID,d);
            meals = new List<MealDetailsViewModel>();
            foreach (Meal i in m)
            {
                var mealVM = new MealDetailsViewModel(_mealService, _foodService, i);
                meals.Add(mealVM);
                totalCals += mealVM.Cals;
                totalMacC += mealVM.MacC;
                totalMacF += mealVM.MacF;
                totalMacP += mealVM.MacP;
            }
            targetCals = user.TargetCals;
            targetMacF = user.TargetMacF;
            targetMacP = user.TargetMacP;
            targetMacC = user.TargetMacC;

            targetMacCTotal = Convert.ToInt32(targetCals * (targetMacC * .01));
            targetMacFTotal = Convert.ToInt32(targetCals * (targetMacF * .01));
            targetMacPTotal = Convert.ToInt32(targetCals * (targetMacP*.01));

            Date = d.ToShortDateString();
        }
    }
}