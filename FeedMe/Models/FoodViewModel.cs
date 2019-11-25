using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FeedMe.Domains;
using FeedMe.Services;

namespace FeedMe.Models
{
    public class FoodViewModel
    {
        FoodService _FoodService;
        IngService _IngService;
        public FoodViewModel(FoodService foodService, IngService ingService)
        {
            _FoodService = foodService;
            _IngService = ingService;
        }

        public FoodViewModel(Food food)
        {
            LoadData(food);
        }

        protected int FoodID { get; set; }

        [Display(Name = "Food Name ")]
        public string FoodName{ get; set; }

        [Display(Name = "Carbs:")]
        public int MacC { get; set; }

        [Display(Name = "Protein:")]
        public int MacP { get; set; }

        [Display(Name = "Fat:")]
        public int MacF { get; set; }

        [Display(Name = "Calories:")]
        public int Cals { get; set; }

        [Display(Name = "Date Created:")]
        public DateTime DateCreated { get; set; }

        public int FoodSlot { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }

        public void LoadData(Food f)
        {
            //Load food data into local vars
            FoodName = f.FoodName;
            MacC = f.MacC;
            MacF = f.MacF;
            MacP = f.MacP;
            Cals = f.Cals;
            DateCreated = f.DateCreated;
            //Food slot?
            //Put ingredients into ingredientviewmodels
            //Ingredients = _IngService.getFoodIngs(FoodID);

        }
    }

    
}