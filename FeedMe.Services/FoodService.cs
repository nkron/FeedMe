using System;
using FeedMe.Repositories;
using FeedMe.Domains;
using System.Collections.Generic;
using FeedMe.Domains.Enumerations;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Food>> Search(string searchName, MealType searchType)
        {
            return await _FoodRepo.Search(searchName, searchType);
        }

        public async void AddFoodToMeal(string date, string mealType, int foodID)
        {
            await _FoodRepo.AddFoodToMeal(date, mealType, foodID);
            return;
        }
    }
}
