using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedMe.Repositories
{
    class MainContext : DbContext
    {
        public MainContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<MainContext>(null);
        }
          
    }
}
