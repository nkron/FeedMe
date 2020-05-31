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
            CalsMax = 2000;
            MealType = 0;
        }
        public List<FoodViewModel> Results { get; set; }
        public string Date { get; set; }
        public string SearchName { get; set; }
        public int CalsMin { get; set; }
        public int CalsMax { get; set; }
        public MealType MealType { get; set; }

        public IEnumerable<SelectListItem> GetTypes()
        {
            yield return new SelectListItem { Text = "Meal Type", Value = "0", Selected = true };
            yield return new SelectListItem { Text = "Breakfast", Value = "1" };
            yield return new SelectListItem { Text = "Lunch", Value = "2" };
            yield return new SelectListItem { Text = "Dinner", Value = "3" };
            yield return new SelectListItem { Text = "Snack", Value = "4" };
        }
    }
}