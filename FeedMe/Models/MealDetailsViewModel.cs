using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FeedMe.Domains;
using FeedMe.Services;

namespace FeedMe.Models
{
    public class MealDetailsViewModel
    {
        MealService _MealService;
        FoodService _FoodService;
        public MealDetailsViewModel(MealService mealService, FoodService foodService)
        {
            _MealService = mealService;
            _FoodService = foodService;
        }
        public MealDetailsViewModel(MealService mealService, FoodService foodService, Meal m)
        {
            _MealService = mealService;
            _FoodService = foodService;
            LoadData(m);
        }

        public MealDetailsViewModel(Meal m)
        {
            LoadData(m);
        }
        protected int MealID { get; set; }

        [Display(Name = "Meal Name ")]
        public string MealName{ get; set; }

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

        public int MealSlot { get; set; }
        public List<FoodViewModel> Foods { get; set; }

        public void LoadData(Meal m)
        {
            //Put meal data in local vars
            MealName = m.MealName;
            MacC = m.MacC;
            MacF = m.MacF;
            MacP = m.MacP;
            Cals = m.Cals;
            DateCreated = m.DateCreated;
            //Meal slot?
            
            //Put each food in a foodviewmodel
            var f = _FoodService.getMealFoods(m.MealID);
            Foods = new List<FoodViewModel>();
            foreach (Food i in f){
                Foods.Add(new FoodViewModel(i));                
            }         
                

        }
    }

    
}