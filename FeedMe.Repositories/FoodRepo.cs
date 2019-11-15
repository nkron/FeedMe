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
        public IEnumerable<Food> GetMealFoods(int MealID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Food>("select * from Foods inner join MealFoods on MealFoods.FoodID = Foods.FoodID where MealFoods.MealID =" + MealID + ";");
                return output;
            }
        }

        public bool AddMealFoods(int MealID, IEnumerable<Food> foodList)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Food>("");
                return true;
            }
        }
    }
}
