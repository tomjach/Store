using Microsoft.AspNetCore.Mvc.Testing;
using Store.Contracts.V1;
using Store.Contracts.V1.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Store.IntegrationTests
{
    public class ProductsControllerTests
    {
        private readonly HttpClient client;

        public ProductsControllerTests()
        {
            var factory = new WebApplicationFactory<Startup>();

            client = factory.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {           

            var response = await client.GetAsync(ApiRoutes.Categories.GetAll);

            var categories = await response.Content.ReadAsAsync<IEnumerable<CategoryResponse>>();
        }

        [Fact]
        public async Task Test2()
        {

            var response = await client.GetAsync(ApiRoutes.Categories.GetAll);

            var categories = await response.Content.ReadAsAsync<IEnumerable<CategoryResponse>>();
        }
    }
}
