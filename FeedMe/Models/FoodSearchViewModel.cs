using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FeedMe.Domains;
using FeedMe.Domains.Enumerations;
using FeedMe.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedMe.Models
{
    public class FoodSearchViewModel
    {
        public FoodSearchViewModel()
        {
            Results = new List<FoodViewModel>();
        }
        //public MealDayViewModel ReferringVM { get; set; }
        public List<FoodViewModel> Results { get; set; }
        public string Date { get; set; }
        public string SearchName { get; set; }
        public MealType MealType { get; set; }

        public IEnumerable<SelectListItem> GetTypes()
        {
            yield return new SelectListItem { Text = "Meal Type", Value = "None" };
            yield return new SelectListItem { Text = "Breakfast", Value = "Breakfast" };
            yield return new SelectListItem { Text = "Lunch", Value = "Lunch" };
            yield return new SelectListItem { Text = "Dinner", Value = "Dinner" };
            yield return new SelectListItem { Text = "Snack", Value = "Snack" };
        }
    }


}