using Store.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface ICategoriesService
    {
        Task<ICollection<Category>> GetAllAsync();
        
        Task<Category> AddAsync(Category category);
    }
}
