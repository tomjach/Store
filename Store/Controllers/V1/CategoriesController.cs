using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Models;
using Store.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoriesService categoriesService, IMapper mapper)
        {
            this.categoriesService = categoriesService;
            this.mapper = mapper;
        }

        [HttpGet(ApiRoutes.Categories.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoriesService.GetAllAsync();

            var response = mapper.Map<ICollection<CategoryResponse>>(categories);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(ApiRoutes.Categories.Add)]
        public async Task<IActionResult> Add([FromBody]CategoryRequest categoryRequest)
        {
            var newCategory = mapper.Map<Category>(categoryRequest);
            
            var category = await categoriesService.AddAsync(newCategory);

            var response = mapper.Map<CategoryResponse>(category);
            
            return Ok(response);
        }
    }
}