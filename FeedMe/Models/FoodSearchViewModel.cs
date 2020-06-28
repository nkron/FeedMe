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
            CalsMin = 100;
            CalsMax = 4000;
            MacCMin = 0;
            MacCMax = 500;
            MacPMin = 0;
            MacPMax = 500;
            MacFMin = 0;
            MacFMax = 500;
            MealType = 0;
        }
        public List<FoodViewModel> Results { get; set; }
        public string Date { get; set; }
        [Display(Name = "Name")]
        public string SearchName { get; set; }
        [Display(Name = "Calories")]
        public int CalsMin { get; set; }
        public int CalsMax { get; set; }
        [Display(Name = "Carbs")]
        public int MacCMin { get; set; }
        public int MacCMax { get; set; }
        [Display(Name = "Protein")]
        public int MacPMin { get; set; }
        public int MacPMax { get; set; }
        [Display(Name = "Fat")]
        public int MacFMin { get; set; }
        public int MacFMax { get; set; }

        [Display(Name = "Type")]
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