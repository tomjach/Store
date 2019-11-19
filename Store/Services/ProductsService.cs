using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models;

namespace Store.Services
{
    public class ProductsService : IProductsService
    {
        private readonly List<Product> products = new List<Product>();

        public ProductsService()
        {
            for (int i = 0; i < 3; i++)
            {
                products.Add(new Product { Id = Guid.NewGuid(), Name = $"Nazwa {i}" });
            }
        }

        public ICollection<Product> GetAll()
        {
            return products;
        }

        public Product Get(Guid id)
        {
            return products.SingleOrDefault(x => x.Id == id);
        }

        public Product Add(string name)
        {
            var product = new Product { Id = Guid.NewGuid(), Name = name };
            products.Add(product);
            return product;
        }
    }
}
