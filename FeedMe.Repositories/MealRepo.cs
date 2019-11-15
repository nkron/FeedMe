using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FeedMe.Domains;


namespace FeedMe.Repositories
{ 
    public class MealRepo
    { 
        public IEnumerable<Meal> GetMeals()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Meals");
                return output;
            }
        }

        public Meal GetByMealID(int ID)
        {            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                Meal output = connection.Query<Meal>("select * from Meals where MealID = " + ID +";").First();
                return output;
            }
        }

        public IEnumerable<Meal> GetHistory(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Meals inner join MealHistory on Meals.MealID = MealHistory.MealID where MealHistory.UserID =" + ID + ";");
                return output;
            }
        }

        public IEnumerable<Meal> AddToHistory(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Meals inner join MealHistory on Meals.MealID = MealHistory.MealID where MealHistory.UserID =" + ID + ";");
                return output;
            }
        }
        public IEnumerable<Meal> GetFavorites(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Meals inner join FavoriteMeals on Meals.MealID = FavoriteMeals.MealID where FavoriteMeals.UserID =" + ID + ";");
                return output;
            }
        }
        public IEnumerable<Meal> AddToFavorites(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Meals inner join FavoriteMeals on Meals.MealID = FavoriteMeals.MealID where FavoriteMeals.UserID =" + ID + ";");
                return output;
            }
        }
    }
}
