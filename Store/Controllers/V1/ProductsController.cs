using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("api/v1/products")]
        public IActionResult GetAll()
        {
            var response = productsService.GetAll().Select(x => new ProductResponse { Id = x.Id, Name = x.Name });

            return Ok(response);
        }

        [HttpGet("api/v1/products/{id}")]
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

        [HttpPost("api/v1/products")]
        public IActionResult Add([FromBody]ProductRequest productRequest)
        {
            var product = productsService.Add(productRequest.Name);

            var response = new ProductResponse { Id = product.Id, Name = product.Name };

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }
    }
}