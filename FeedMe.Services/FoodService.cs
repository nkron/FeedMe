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

        public async Task<IEnumerable<Food>> Search(string searchName, MealType searchType, int calsMin, int calsMax)
        {
            return await _FoodRepo.Search(searchName, searchType, calsMin, calsMax);
        }

        public void AddFoodToExistingMeal(int foodID, int mealID)
        {
            _FoodRepo.AddFoodToExistingMeal(foodID,mealID);
            return;
        }
        public void AddFoodToNewMeal(string date, int mealType, int foodID, int userID)
        {
            _FoodRepo.AddFoodToNewMeal(date, mealType, foodID, userID);
            return;
        }
        public void RemoveFoodFromMeal(int mealID, int foodID)
        {
            _FoodRepo.RemoveFoodFromMeal(mealID, foodID);
            return;
        }
        public void UpdateFood(string foodName, string foodDesc,int cals, int? macC, int? macF, int? macP, int foodID)
        {
            _FoodRepo.UpdateFood(foodName,foodDesc, cals, macC, macF, macP, foodID);
        }
        public int CreateFood(string foodName, string foodDesc, int cals, int? macC, int? macF, int? macP, int creatorID)
        {
            return _FoodRepo.CreateFood(foodName, foodDesc, cals, macC, macF, macP, creatorID);
        }

        public Task<IEnumerable<FoodAPI>> SearchAPI()
        {
            var a = APIHelper.GetFoods();
            return a;
        }
    }
}
