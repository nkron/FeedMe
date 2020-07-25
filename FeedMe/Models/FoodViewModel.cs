using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FeedMe.Domains;
using FeedMe.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedMe.Models
{
    public class FoodViewModel
    {
        private readonly FoodService _FoodService;
        private readonly UserManager<User> _userManager;
        public FoodViewModel(FoodService foodService, UserManager<User> userManager)
        {
            _FoodService = foodService;
            _userManager = userManager;
        }

        public FoodViewModel(Food food)
        {
            LoadData(food);
        }

        public FoodViewModel(Food food, int servings)
        {
            LoadData(food);
            Servings = servings;
        }

        public FoodViewModel()
        {

        }

        [HiddenInput]
        public int FoodID { get; set; }
        [HiddenInput]
        public string APIFoodID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string FoodName{ get; set; }
        [Display(Name = "Description")]
        public string FoodDesc { get; set; }
        [Display(Name = "Brand")]
        public string Brand { get; set; }
        [Display(Name = "Carbs(g)")]
        [Range(0,10000)]
        public int? MacC { get; set; }

        [Display(Name = "Protein(g)")]
        [Range(0, 10000)]
        public int? MacP { get; set; }

        [Display(Name = "Fat(g)")]
        [Range(0, 10000)]
        public int? MacF { get; set; }
        [Required]
        [Display(Name = "Calories")]
        [Range(0, 10000)]
        public int Cals { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        
        [Display(Name = "created by ")]
        public string CreatorUsername { get; set; }
        public int CreatorID { get; set; }
        public string ImageURL { get; set; }
        public int FoodType { get; set; }
        
        //optional field for relating servings in meal plan
        public int? Servings { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public Food BaseFood { get; set; }  
        public void LoadData(Food f)
        {
            //Load food data into local vars
            FoodID = f.FoodID;
            Brand = f.Brand;
            APIFoodID = f.APIFoodID;
            FoodName = f.FoodName;
            MacC = f.MacC;
            MacF = f.MacF;
            MacP = f.MacP;
            Cals = f.Cals;
            DateCreated = f.DateCreated;
            CreatorID = f.CreatorID;
            if (f.ImageURL==null) {
                ImageURL = "https://i.imgur.com/e1IsvyR.png";
            }
            else
            {
                ImageURL = f.ImageURL;
            }
            BaseFood = f;
            //todo:add username
            //CreatorUsername = _userManager.FindByIdAsync(f.CreatorID.ToString()).Result.Username;

        }       
    }

    
}