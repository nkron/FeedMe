using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
        //Get all meals for user
        public IEnumerable<Meal> GetUserMeals(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("select * from Meals inner join UserMeals on Meals.MealID = UserMeals.MealID where UserMeals.UserID =" + ID + ";");
                return output;
            }
        }

        public async Task<IEnumerable<Meal>> GetUserMealsByDate(int ID, DateTime date)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var param = new DynamicParameters();
                param.Add("@UserID", ID,DbType.Int32);
                param.Add("@DateUsed", date,DbType.Date);

                var output = await connection.QueryAsync<Meal>(
                    "dbo.GetUserMealsByDate",
                    param,
                    commandType: CommandType.StoredProcedure);

                //string cleanDate = date.Year.ToString()+"-"+ + date.Month +"-"+ date.Day;
                //var output = connection.Query<Meal>("dbo.GetUserMealsByDate @UserID @DateUsed", new { UserID = ID , DateUsed = date});
                return output;
            }
        }

        public IEnumerable<Meal> AddToHistory(int ID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Meal>("");
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
