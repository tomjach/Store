using Store.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.App.Services
{
    public interface IProductsService
    {
        Task<ICollection<ProductViewModel>> GetAllAsync();
        Task<bool> AddAsync(CreateProductViewModel createProductViewModel);
    }
}
