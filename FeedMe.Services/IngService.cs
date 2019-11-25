using System;
using FeedMe.Repositories;
using FeedMe.Domains;
using System.Collections.Generic;

namespace FeedMe.Services
{
    public class IngService
    {
        private readonly IngRepo _IngRepo;
        public IngService(IngRepo IngRepo)
        {
            _IngRepo = IngRepo;
        }

        public Ingredient getByID(int IngID)
        {
            return _IngRepo.GetByIngID(IngID);
        }
        public IEnumerable<Ingredient> getFoodIngs(int FoodID)
        {
            return _IngRepo.GetFoodIngs(FoodID);
        }
    }
}
