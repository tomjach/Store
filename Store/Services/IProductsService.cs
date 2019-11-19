using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface IProductsService 
    {
        ICollection<Product> GetAll();

        Product Get(Guid id);

        Product Add(string name);
    }
}
