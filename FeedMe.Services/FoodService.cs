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
        public List<KeyValuePair<Food, int>> GetMealFoods(int MealID)
        {
            return _FoodRepo.GetMealFoods(MealID);
        }

        public async Task<IEnumerable<Food>> Search(string searchName, string brand, int calsMin, int calsMax, int macCMin, int macCMax, int macPMin, int macPMax, int macFMin, int macFMax)
        {
            return await _FoodRepo.Search(searchName, brand, calsMin, calsMax, macCMin, macCMax, macPMin, macPMax, macFMin, macFMax);
        }

        public void AddFoodToExistingMeal(int foodID, int mealID, string APIFoodID)
        {
            _FoodRepo.AddFoodToExistingMeal(foodID, mealID, APIFoodID);
            return;
        }
        public void AddFoodToNewMeal(string date, int mealType, int foodID, string APIFoodID, int userID)
        {
            _FoodRepo.AddFoodToNewMeal(date, mealType, foodID, APIFoodID, userID);
            return;
        }
        public void RemoveFoodFromMeal(int mealID, int foodID, string APIFoodID)
        {
            _FoodRepo.RemoveFoodFromMeal(mealID, foodID, APIFoodID);
            return;
        }
        public void UpdateFood(string foodName, string foodDesc, string brand, int cals, int? macC, int? macF, int? macP, int foodID)
        {
            _FoodRepo.UpdateFood(foodName, brand, foodDesc, cals, macC, macF, macP, foodID);
        }
        public int CreateFood(string foodName, string foodDesc, string brand, int cals, int? macC, int? macF, int? macP, int creatorID, string APIFoodID, string ImageURL)
        {
            return _FoodRepo.CreateFood(foodName, foodDesc, brand, cals, macC, macF, macP, creatorID,APIFoodID,ImageURL);
        }
        public int GetAPIFoodInLocal(string apiFoodID)
        {
            return _FoodRepo.GetAPIFoodInLocal(apiFoodID);
        }
    }
}
