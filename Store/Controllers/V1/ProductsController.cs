using Microsoft.AspNetCore.Mvc;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Controllers.V1
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet(ApiRoutes.Products.GetAll)]
        public IActionResult GetAll()
        {
            var response = productsService.GetAll().Select(x => new ProductResponse { Id = x.Id, Name = x.Name });

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Products.Get)]
        public IActionResult Get([FromRoute]Guid id)
        {
            var product = productsService.Get(id);

            if(product == null)
            {
                return NotFound();
            }

            var response = new ProductResponse { Id = product.Id, Name = product.Name };

            return Ok(response);
        }

        [HttpPost(ApiRoutes.Products.Add)]
        public IActionResult Add([FromBody]ProductRequest productRequest)
        {
            var product = productsService.Add(productRequest.Name);

            var response = new ProductResponse { Id = product.Id, Name = product.Name };

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut(ApiRoutes.Products.Update)]
        public IActionResult Update([FromRoute] Guid id, [FromBody]ProductRequest productRequest)
        {
            var product = productsService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productRequest.Name;

            var updatedProduct = productsService.Update(product);

            var response = new ProductResponse { Id = updatedProduct.Id, Name = updatedProduct.Name };

            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Products.Delete)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var deleted = productsService.Delete(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}