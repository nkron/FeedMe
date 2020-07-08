using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using FeedMe.Domains;

namespace FeedMe.Repositories
{
    public static class APIHelper
    {
        private static HttpClient client = new HttpClient();
        public async static Task<IEnumerable<FoodAPI>> GetFoods()
        {
            string Uri = "https://api.edamam.com/api/food-database/v2/parser?app_id=103664a0&app_key=b1ce1d8f2c9a98f79d67f9e3cb070d3a&ingr=chicken tenders";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Uri);
            HttpResponseMessage response = new HttpResponseMessage();

            
            response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<FoodAPI>>();                
            }
            else
            {
                return null;
            }
            }
    }
}
