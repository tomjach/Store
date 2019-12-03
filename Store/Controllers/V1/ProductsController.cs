using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Helpers;
using Store.Models;
using Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly IMapper mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
        }

        [HttpGet(ApiRoutes.Products.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var products = await productsService.GetAllAsync();

            //var response = products.Select(x => new ProductResponse { Id = x.Id, Name = x.Name });
            var response = mapper.Map<ICollection<ProductResponse>>(products);

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Products.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var product = await productsService.GetAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            var response = new ProductResponse { Id = product.Id, Name = product.Name };

            return Ok(response);
        }

        [HttpPost(ApiRoutes.Products.Add)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody]ProductRequest productRequest)
        {

            //var newProduct = new Product()
            //{
            //    Name = productRequest.Name,
            //    OwnerUserId = userId,
            //    CategoryId = productRequest.CategoryId
            //};
            var newProduct = mapper.Map<Product>(productRequest);


            var product = await productsService.AddAsync(newProduct);



            //var response = new ProductResponse
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    CategoryId = product.CategoryId,
            //    CategoryName = product.Category.Name
            //};
            var response = mapper.Map<ProductResponse>(product);
            
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut(ApiRoutes.Products.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]ProductRequest productRequest)
        {
            var product = await productsService.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            //product.Name = productRequest.Name;
            //product.CategoryId = productRequest.CategoryId;
            mapper.Map(productRequest, product);
                  
            var updatedProduct = await productsService.UpdateAsync(product);

            //var response = new ProductResponse { Id = updatedProduct.Id, Name = updatedProduct.Name };
            var response = mapper.Map<ProductResponse>(updatedProduct);

            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Products.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await productsService.DeleteAsync(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}