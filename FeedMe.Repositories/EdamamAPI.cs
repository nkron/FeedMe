using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace FeedMe.Repositories
{
    public class EdamamAPI
    {
        public async Task SearchFood(string name = "")
        {
            string url = "https://api.edamam.com/api/food-database/v2/parser?app_id=103664a0&app_key=b1ce1d8f2c9a98f79d67f9e3cb070d3a";

            url += "&ingr=" + name;

        }

    }
}