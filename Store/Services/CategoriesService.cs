using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly DataContext dbContext;

        public CategoriesService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<Category>> GetAllAsync(PaginationFilter paginationFilter)
        {
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await dbContext.Categories.Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }

        public async Task<Category> AddAsync(Category category)
        {
            dbContext.Categories.Add(category);

            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
