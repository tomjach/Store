using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Store.App.Helpers;
using Store.App.Models;

namespace Store.App.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;

        public ProductsService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IOptions<StoreApi> options)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(options.Value.Address);
        }

        public async Task<ICollection<ProductViewModel>> GetAllAsync()
        {
            var token = httpContextAccessor.HttpContext.Request.Cookies["api_token"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("/api/v1/products");
            var products = await response.Content.ReadAsAsync<ICollection<ProductViewModel>>();

            return products;
        }
    }
}
