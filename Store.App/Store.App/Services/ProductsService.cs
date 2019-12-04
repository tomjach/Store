using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Store.App.Helpers;
using Store.App.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.App.Services
{
    public class ProductsService : BaseService, IProductsService
    {
        public ProductsService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IOptions<StoreApi> options) 
            : base(httpContextAccessor, httpClientFactory, options)
        {
        }

        public async Task<ICollection<ProductViewModel>> GetAllAsync()
        {
            var response = await HttpClient.GetAsync("/api/v1/products");
            var products = await response.Content.ReadAsAsync<ICollection<ProductViewModel>>();

            return products;
        }

        public async Task<bool> AddAsync(CreateProductViewModel createProductViewModel)
        {
            var response = await HttpClient.PostAsJsonAsync("/api/v1/products", createProductViewModel);

            return response.StatusCode == HttpStatusCode.Created;
        }
    }
}
