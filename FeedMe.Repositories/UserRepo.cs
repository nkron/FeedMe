using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FeedMe.Domains;


namespace FeedMe.Repositories
{ 
    public class UserRepo
    { 
        public IEnumerable<User> GetUsers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                var output = connection.Query<User>("select * from Users");
                return output;
            }
        }

        public User GetByID(int ID)
        {//Helper.CnnValue("FeedMeDB")
            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnValue("FeedMeDB")))
            {
                User output = connection.Query<User>("select * from Users where UserID = " + ID +";").First();
                return output;
            }
        }
    }
}
