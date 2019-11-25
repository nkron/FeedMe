using System;
using FeedMe.Repositories;
using FeedMe.Domains;
using System.Collections.Generic;

namespace FeedMe.Services
{
    public class FoodService
    {
        private readonly FoodRepo _FoodRepo;
        public FoodService(FoodRepo FoodRepo)
        {
            _FoodRepo = FoodRepo;
        }

        public Food getByID(int FoodID)
        {
            return _FoodRepo.GetByFoodID(FoodID);
        }
        public IEnumerable<Food> getMealFoods(int MealID)
        {
            return _FoodRepo.GetMealFoods(MealID);
        }
    }
}
