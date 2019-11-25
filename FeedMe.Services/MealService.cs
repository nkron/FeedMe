using System;
using FeedMe.Repositories;
using FeedMe.Domains;
using System.Collections.Generic;

namespace FeedMe.Services
{
    public class MealService
    {
        private readonly MealRepo _mealRepo;
        public MealService(MealRepo mealRepo)
        {
            _mealRepo = mealRepo;
        }

        public Meal getByID(int MealID)
        {
            return _mealRepo.GetByMealID(MealID);
        }
        public IEnumerable<Meal> getUserMealsByDate(int UserID, DateTime dateUsed)
        {
            return _mealRepo.GetUserMealsByDate(UserID, dateUsed).Result;
        }
    }
}
