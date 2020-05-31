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

        public async Task<IEnumerable<Food>> Search(string searchName, MealType searchType, int calsMin, int calsMax)
        {
            return await _FoodRepo.Search(searchName, searchType, calsMin, calsMax);
        }

        public void AddFoodToMeal(string date, int mealType, int foodID, int userID)
        {
            _FoodRepo.AddFoodToMeal(date, mealType, foodID, userID);
            return;
        }

        public void UpdateFood(string foodName, int cals, int? macC, int? macF, int? macP, int? foodID)
        {
            _FoodRepo.UpdateFood(foodName, cals, macC, macF, macP, foodID);
        }
    }
}
