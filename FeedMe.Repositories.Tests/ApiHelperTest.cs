﻿using System;
using System.Net.Http;
using Xunit;

namespace FeedMe.Repositories.Tests
{
    public class ApiHelperTest
    {
        private static HttpClient client = new HttpClient();
        [Fact]
        public async void ApiHelper_CanConnectToEdamamAPI()
        {
            //Arrange
            string Uri = "https://api.edamam.com/api/food-database/v2/parser?app_id=103664a0&app_key=b1ce1d8f2c9a98f79d67f9e3cb070d3a";
            //Act
            HttpResponseMessage response = await client.GetAsync(Uri);
            //Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
