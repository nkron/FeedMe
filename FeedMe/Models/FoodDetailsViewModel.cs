using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FeedMe.Domains;
using FeedMe.Services;
using FeedMe.Web.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedMe.Models
{
    public class FoodDetailsViewModel : FoodViewModel
    {
        private readonly FoodService _FoodService;
        private readonly IngService _IngService;
        private readonly UserManager<User> _userManager;
        public FoodDetailsViewModel(FoodService foodService, IngService ingService, UserManager<User> userManager)
        {
            _FoodService = foodService;
            _IngService = ingService;
            _userManager = userManager;

            if(MacC.HasValue && MacF.HasValue && MacP.HasValue) { 
            int total = MacC.Value + MacF.Value + MacP.Value;

                SliderNum1 = MacC.Value / total;
                SliderNum2 = MacF.Value + SliderNum1;
            }

        }

        public FoodDetailsViewModel(Food food)
        {
            LoadData(food);
        }

        public FoodDetailsViewModel()
        {

        }

        public int SliderNum1 { get; set; }

        public int SliderNum2 { get; set; }

        public SubmitType SubmitType { get; set; }

        
    }
    
}