using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FeedMe.Domains;


namespace FeedMe.Repositories
{ 
    public class IngRepo
    { 
        public Ingredient GetByIngID(int IngID)
        {            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                Ingredient output = connection.Query<Ingredient>("select * from Ings where IngID = " + IngID +";").First();
                return output;
            }
        }
        public IEnumerable<Ingredient> GetFoodIngs(int FoodID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<Ingredient>("dbo.GetFoodIngs @FoodID ", new { IngID = FoodID});
                return output;
            }
        }
    }
}
