using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using System.Collections.Generic;
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

        public async Task<ICollection<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> AddAsync(Category category)
        {
            dbContext.Categories.Add(category);

            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
