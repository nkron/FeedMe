using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace FeedMe.Repositories
{
    class Helper
    {
        public static string CnnValue(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
