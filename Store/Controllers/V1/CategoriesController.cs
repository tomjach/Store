using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Helpers;
using Store.Models;
using Store.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoriesService categoriesService, IMapper mapper)
        {
            this.categoriesService = categoriesService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Pobiera wszystkie kategorie
        /// </summary>
        [HttpGet(ApiRoutes.Categories.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery]PaginationRequest paginationRequest)
        {
            var paginationFilter = mapper.Map<PaginationFilter>(paginationRequest);

            var categories = await categoriesService.GetAllAsync(paginationFilter);

            var response = PaginationHelper.CreateResponse(
                paginationFilter,
                mapper.Map<ICollection<CategoryResponse>>(categories),
                HttpContext,
                ApiRoutes.Categories.GetAll);

            return Ok(response);
        }

        /// <summary>
        /// Dodaje nową kategorię
        /// </summary>
        /// <response code="200">Zwraca utworzoną kategorię</response>
        /// <response code="400">Błąd walidacji</response>
        /// <response code="401">Użytkownik niezalogowany</response>
        /// <response code="403">Brak dostępu</response>
        [Authorize(Roles = "Admin")]
        [HttpPost(ApiRoutes.Categories.Add)]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Add([FromBody]CategoryRequest categoryRequest)
        {
            var newCategory = mapper.Map<Category>(categoryRequest);
            
            var category = await categoriesService.AddAsync(newCategory);

            var response = mapper.Map<CategoryResponse>(category);
            
            return Ok(response);
        }
    }
}