using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Store.App.Helpers;
using Store.App.Models;
using Store.App.StoreApiContracts.Respones;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.App.Services
{
    public class CategoriesService : BaseService, ICategoriesService
    {
        public CategoriesService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IOptions<StoreApi> options) 
            : base(httpContextAccessor, httpClientFactory, options)
        {
        }
        
        public async Task<ICollection<CategoryViewModel>> GetAllAsync()
        {
            var response = await HttpClient.GetAsync("/api/v1/categories");
            var categries = await response.Content.ReadAsAsync<PagedResponse<CategoryResponse>>();

            var categoriesViewModel = categries.Data.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return categoriesViewModel;
        }
    }
}
