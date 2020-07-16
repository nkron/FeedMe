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
        public List<KeyValuePair<Food, int>> GetMealFoods(int mealID)
        {
            //Returns Foods and associated Servings

            //Get foodIDs and servings from mealfoods that are api-only

            //Get list of foods from apihelper
            //Combine food list and servings to kvp
            //Run same process for foods stored in feedme db
            //combine kvps

            List<KeyValuePair<Food,int>> list = new List<KeyValuePair<Food,int>>();
            List<int> servings;
            List<Food> foods;
            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                foods = (List<Food>)connection.Query<Food>("dbo.GetFoodsForMeal @MealID ", new { MealID = mealID });
                
                servings = (List<int>)connection.Query<int>("dbo.GetFoodServings @MealID ", new { MealID = mealID });
            }

            for (int i = 0;i<foods.Count;i++)
            {
                list.Add(new KeyValuePair<Food, int>(foods[i],servings[i]));
            }

            return list;
        }
        //Returns 0 if API food isn't associated with a food in DB
        public int GetAPIFoodInLocal(string @APIFoodId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                return connection.Query<int>("dbo.GetAPIFoodIfExists @APIFoodID ", new { APIFoodID = @APIFoodId}).FirstOrDefault();
            }
        }
        public void AddFoodToNewMeal(string date, int mealType, int foodID, string APIFoodID, int userID)
        {           
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                //Add logic to add api foods (also update db for this?)
                var param = new DynamicParameters();
                param.Add("@DateUsed", date, DbType.Date);
                param.Add("@MealType", mealType, DbType.Int32);
                param.Add("@FoodID", foodID, DbType.Int32);
                param.Add("@UserID", userID, DbType.Int32);
                param.Add("@APIFoodID", APIFoodID, DbType.String);
                var i = connection.Execute(
                    "dbo.AddFoodToNewMeal",
                    param,
                    commandType: CommandType.StoredProcedure);
                return;
            }
        }
        public void AddFoodToExistingMeal(int foodID, int mealID, string APIFoodID)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {

                var param = new DynamicParameters();
                param.Add("@MealID", mealID, DbType.Int32);
                param.Add("@FoodID", foodID, DbType.Int32);
                param.Add("@APIFoodID", APIFoodID, DbType.String);

                var i = connection.Execute(
                    "dbo.AddFoodToExistingMeal",
                    param,
                    commandType: CommandType.StoredProcedure);
                return;
            }
        }
        public void RemoveFoodFromMeal(int mealID, int foodID, string APIFoodID)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {

                var param = new DynamicParameters();
                param.Add("@FoodID", foodID, DbType.Int32);
                param.Add("@APIFoodID", APIFoodID, DbType.String);
                param.Add("@MealID", mealID, DbType.Int32);
                var i = connection.Execute(
                    "dbo.DeleteFoodFromMeal",
                    param,
                    commandType: CommandType.StoredProcedure);
                return;
            }
        }


        public void UpdateFood(string foodName, string foodDesc, int cals, int? macC, int? macF, int? macP, int foodID)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                string procedureName = "dbo.UpdateFood";

                var param = new DynamicParameters();
                param.Add("@FoodName", foodName, DbType.String);
                param.Add("@FoodDesc", foodDesc, DbType.String);
                param.Add("@Cals", cals, DbType.Int32);
                param.Add("@MacC", macC, DbType.Int32);
                param.Add("@MacP", macP, DbType.Int32);
                param.Add("@MacF", macF, DbType.Int32);
                param.Add("@FoodID", foodID, DbType.Int32);            

                var i = connection.Execute(
                    procedureName,
                    param,
                    commandType: CommandType.StoredProcedure);
                return;
            }
        }
        public int CreateFood(string foodName, string foodDesc, int cals, int? macC, int? macF, int? macP, int creatorID, string APIFoodID, string ImageURL)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                string procedureName = "dbo.CreateFood";

                var param = new DynamicParameters();
                param.Add("@FoodName", foodName, DbType.String);
                param.Add("@FoodDesc", foodDesc, DbType.String);
                param.Add("@Cals", cals, DbType.Int32);
                param.Add("@MacC", macC, DbType.Int32);
                param.Add("@MacP", macP, DbType.Int32);
                param.Add("@MacF", macF, DbType.Int32);
                param.Add("@CreatorID", creatorID, DbType.Int32);
                param.Add("@APIFoodID", APIFoodID, DbType.String);
                param.Add("@ImageURL", ImageURL, DbType.String);
                return connection.Query<int>(
                    procedureName,
                    param,
                    commandType: CommandType.StoredProcedure).First();               
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

        //Needs to be updated
        public IEnumerable<Meal> GetFavorites(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Foods inner join FavoriteFoods on Foods.FoodID = FavoriteFoods.FoodID where FavoriteFoods.UserID =" + ID + ";");
                return output;
            }
        }
        //Needs to be updated
        public IEnumerable<Meal> AddToFavorites(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Foods inner join FavoriteFoods on Foods.FoodID = FavoriteFoods.FoodID where FavoriteFoods.UserID =" + ID + ";");
                return output;
            }
        }
    }
}
