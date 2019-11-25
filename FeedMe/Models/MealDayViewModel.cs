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
        public String Date{get; set;}
        
        public List<MealDetailsViewModel> meals { get; set; }

        public MealDayViewModel(MealService mealService, FoodService _foodService, int userID, DateTime d)
        {
            _mealService = mealService;
            
            //Put each meal in a mealviewmodel
            var m = _mealService.getUserMealsByDate(userID,d);
            meals = new List<MealDetailsViewModel>();
            foreach (Meal i in m)
            {
                meals.Add(new MealDetailsViewModel(_mealService,_foodService,i));
            }

            Date = d.ToShortDateString();
        }
    }
}