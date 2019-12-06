using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FeedMe.Domains;


namespace FeedMe.Repositories
{ 
    public class FoodRepo
    { 
        public Food GetByFoodID(int FoodID)
        {            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                Food output = connection.Query<Food>("select * from Foods where FoodID = " + FoodID +";").First();
                return output;
            }
        }
        public IEnumerable<Food> GetMealFoods(int mealID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Food>("dbo.GetFoods @MealID ", new { MealID = mealID});
                return output;
            }
        }

        public IEnumerable<Food> AddFood(Food f)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Food>("");
                return output;
            }
        }

        public IEnumerable<Meal> GetFavorites(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Foods inner join FavoriteFoods on Foods.FoodID = FavoriteFoods.FoodID where FavoriteFoods.UserID =" + ID + ";");
                return output;
            }
        }
        public IEnumerable<Meal> AddToFavorites(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Foods inner join FavoriteFoods on Foods.FoodID = FavoriteFoods.FoodID where FavoriteFoods.UserID =" + ID + ";");
                return output;
            }
        }
        //public bool AddMealFoods(int mealID, List<int> foodIDs)
        //{
        //    using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
        //    {
        //        foreach(int i in foodIDs){
        //            connection.Execute(("dbo.AddMealFood @MealID, @FoodID", new { MealID = mealID, FoodID = i }));
        //        }
        //        var output = connection.Query(("dbo.GetUser @UserID", new { meal = ID }));
        //        return true;
        //    }
        //}
    }
}
