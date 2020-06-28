using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FeedMe.Domains;
using FeedMe.Domains.Enumerations;
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
        public int MealID { get; set; }

        [Display(Name = "Meal Name")]
        public string MealName { get; set; }

        [Display(Name = "Carbs")]
        public int MacC { get; set; }

        [Display(Name = "Protein")]
        public int MacP { get; set; }

        [Display(Name = "Fat")]
        public int MacF { get; set; }

        [Display(Name = "Calories")]
        public int Cals { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public DateTime DateUsed { get; set; }

        public int MealType { get; set; }
        public List<FoodViewModel> Foods { get; set; }

        public void LoadData(Meal m)
        {
            //Put meal data in local vars
            MealID = m.MealID;
            MealName = m.MealName;
            MacC = m.MacC;
            MacF = m.MacF;
            MacP = m.MacP;
            DateUsed = m.DateUsed;
            MealType = m.MealType;


            //Put each food in a foodviewmodel
            List<KeyValuePair<Food, int>> list = _FoodService.GetMealFoods(m.MealID);

            Foods = new List<FoodViewModel>();
            foreach (KeyValuePair<Food, int> kvp in list)
            {

                Food f = kvp.Key;
                int s = kvp.Value;

                Cals += f.Cals*s;
                MacF += f.MacF*s;
                MacP += f.MacP*s;
                MacC += f.MacC*s;

                FoodViewModel fvm = new FoodViewModel(f);
                fvm.Servings = s;

                Foods.Add(fvm);
            }
        }
    }


}