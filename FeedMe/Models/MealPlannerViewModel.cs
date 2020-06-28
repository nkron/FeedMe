using System;
using System.ComponentModel.DataAnnotations;
using FeedMe.Services;
using FeedMe.Domains;
using System.Collections.Generic;
using System.Globalization;

namespace FeedMe.Models
{
    public class MealPlannerViewModel
    {
        MealService _mealService;
        MealService _foodService;

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
        public String Month { get; set; }
        public String Year { get; set; }
        public String Day { get; set; }
        public String DayWord { get; set; }
        public List<MealDetailsViewModel> meals { get; set; }

        public MealPlannerViewModel()
        {

        }
        public MealPlannerViewModel(MealService mealService, FoodService _foodService, User user, DateTime d)
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

            Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(d.Month);
            Year = d.Year.ToString();
            Day = d.Day.ToString();
            DayWord = d.DayOfWeek.ToString();
        }
    }
}