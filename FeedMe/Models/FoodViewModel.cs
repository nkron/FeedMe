using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FeedMe.Domains;
using FeedMe.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedMe.Models
{
    public class FoodViewModel
    {
        private readonly FoodService _FoodService;
        private readonly IngService _IngService;
        public FoodViewModel(FoodService foodService, IngService ingService)
        {
            _FoodService = foodService;
            _IngService = ingService;
        }

        public FoodViewModel(Food food)
        {
            LoadData(food);
        }

        public FoodViewModel()
        {

        }

        public int FoodID { get; set; }
        [Required]
        [Display(Name = "Food Name ")]
        public string FoodName{ get; set; }

        [Display(Name = "Carbs(g)")]
        public int? MacC { get; set; }

        [Display(Name = "Protein(g)")]
        public int? MacP { get; set; }

        [Display(Name = "Fat(g)")]
        public int? MacF { get; set; }
        [Required]
        [Display(Name = "Calories")]
        public int Cals { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        public int FoodType { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public string Tooltip
        {
            get
            {
                string s = (
                    "<span><b> Calories: " + Cals + "</b></span><br>" +
                    "<span style='color: #FCB524'> Carbs:" + MacC + "</span><br>" +
                    "<span style='color: #52C0BC'> Protein:" + MacP + "</span><br>" +
                    "<span style='color: #976fe8'> Fat:" + MacF + "</span>"

                );
                return s;
            }
        }
        public void LoadData(Food f)
        {
            //Load food data into local vars
            FoodID = f.FoodID;
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