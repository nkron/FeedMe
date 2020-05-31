using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FeedMe.Domains;
using FeedMe.Domains.Enumerations;

namespace FeedMe.Repositories
{
    public class FoodRepo
    {
        public Food GetByFoodID(int FoodID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                Food output = connection.Query<Food>("select * from Foods where FoodID = " + FoodID + ";").First();
                return output;
            }
        }
        public IEnumerable<Food> GetMealFoods(int mealID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Food>("dbo.GetFoods @MealID ", new { MealID = mealID });
                return output;
            }
        }

        public void AddFoodToMeal(string date, int mealType, int foodID, int userID)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {

                var param = new DynamicParameters();
                param.Add("@DateUsed", date, DbType.Date);
                param.Add("@MealType", mealType, DbType.Int32);
                param.Add("@FoodID", foodID, DbType.Int32);
                param.Add("@UserID", userID, DbType.Int32);

                var i = connection.Execute(
                    "dbo.AddFoodToMeal",
                    param,
                    commandType: CommandType.StoredProcedure);
                return;
            }
        }

        public void UpdateFood(string foodName, int cals, int? macC, int? macF, int? macP, int? foodID)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                string procedureName = "dbo.CreateFood";

                var param = new DynamicParameters();
                param.Add("@FoodName", foodName, DbType.String);
                param.Add("@Cals", cals, DbType.Int32);
                param.Add("@MacC", macC, DbType.Int32);
                param.Add("@MacP", macP, DbType.Int32);
                param.Add("@MacF", macF, DbType.Int32);
                if (foodID != null)
                {
                    param.Add("@FoodID", foodID, DbType.Int32);
                    procedureName = "dbo.UpdateFood";
                }

                var i = connection.Execute(
                    procedureName,
                    param,
                    commandType: CommandType.StoredProcedure);
                return;
            }
        }

        public async Task<IEnumerable<Food>> Search(string searchName, MealType? searchType, int CalsMin, int CalsMax)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                
                var param = new DynamicParameters();
                param.Add("@FoodName", "%" + searchName + "%", DbType.String);

                if (searchType == 0)
                    searchType = null;
                param.Add("@FoodType", searchType, DbType.Int32);
                param.Add("@CalsMin", CalsMin, DbType.Int32);
                param.Add("@CalsMax", CalsMax, DbType.Int32);
                await connection.OpenAsync();
                var i =  await connection.QueryAsync<Food>(
                    "dbo.SearchFood",
                    param,
                    commandType: CommandType.StoredProcedure);

                return i;

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
