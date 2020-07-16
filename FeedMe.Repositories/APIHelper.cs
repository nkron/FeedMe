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

        public async static Task<List<Food>> SearchFood(string foodName)
        {
            string Uri = "https://api.edamam.com/api/food-database/v2/parser?app_id=103664a0&app_key=b1ce1d8f2c9a98f79d67f9e3cb070d3a";
            if (foodName == null)
            {
                //requires a value and dont know what else to do for default
                Uri += "&ingr=a";
            }
            else
            {
                Uri += "&ingr=" + foodName;
            }
            HttpResponseMessage response = await client.GetAsync(Uri);

            if (response.IsSuccessStatusCode)
            {
                APIFoods f = JsonConvert.DeserializeObject<APIFoods>(response.Content.ReadAsStringAsync().Result);
                return ConvertAPIFoodsToFood(f);
            }
            else
            {
                throw new Exception("Application failed to access API at address: " + Uri);
            }
        }
        private static List<Food> ConvertAPIFoodsToFood(APIFoods FoodsIn)
        {
            List<Food> f = new List<Food>();
            for (int i = 0; i < FoodsIn.hints.Length; i++)
            {
                f.Add(new Food(FoodsIn.hints[i].food));
            }
            return f;
        }
        private static Food ConvertAPIFoodsToFood(APIFoodNutrients FoodsIn)
        {
                return  new Food(FoodsIn);
         
        }
        public static CatFact GetCatFact()
        {
            string Uri = "https://cat-fact.herokuapp.com/facts/random";
            HttpResponseMessage response = new HttpResponseMessage();

            response = client.GetAsync(Uri).Result;

            if (response.IsSuccessStatusCode)
            {
                CatFact output = response.Content.ReadFromJsonAsync<CatFact>().Result;
                return output;
            }
            else
            {
                throw new Exception("Application failed to access API at address: " + Uri);
            }
        }

        //Cannot get foods by ID because of API BS. Need to pass all available measurements to even get basic info
        //public async static Task<Food> GetFoodByID(string id)
        //{
        //    string Uri = "https://api.edamam.com/api/food-database/v2/nutrients?app_id=103664a0&app_key=b1ce1d8f2c9a98f79d67f9e3cb070d3a";
        //    var values = new List<KeyValuePair<string, string>>();
        //    values.Add(new KeyValuePair<string, string>("quantity", "1"));
        //    values.Add(new KeyValuePair<string, string>("measureURI", "http://www.edamam.com/ontologies/edamam.owl#Measure_serving"));
        //    values.Add(new KeyValuePair<string, string>("foodID", "food_b5sq39gblijoq7br7zwkiby5nf22"));
        //    var content = new FormUrlEncodedContent(values);

        //    string data = @"{""ingredients"":[{""quantity"":1,""measureURI"":""http://www.edamam.com/ontologies/edamam.owl#Measure_serving"",""foodId"":"+ (char)34 + id + (char)34 +"}]}";



        //    var response = await client.PostAsJsonAsync(Uri, data);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        APIFoodNutrients f = JsonConvert.DeserializeObject<APIFoodNutrients>(response.Content.ReadAsStringAsync().Result);
        //        return ConvertAPIFoodsToFood(f);
        //    }
        //    else
        //    {
                
        //        throw new Exception("Application failed to access API at address: " + Uri + " : "+ response.StatusCode.ToString());
        //    }
        //}
    }
}

