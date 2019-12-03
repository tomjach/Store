using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Helpers;
using Store.Models;

namespace Store.Services
{
    public class ProductsService : IProductsService
    {
        private readonly DataContext dbContext;
        private readonly IUser user;

        public ProductsService(DataContext dbContext, IUser user)
        {
            this.dbContext = dbContext;
            this.user = user;
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await dbContext.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await dbContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.OwnerUserId = user.Id;

            dbContext.Products.Add(product);

            await dbContext.SaveChangesAsync();

            var productToReturn = await GetAsync(product.Id);
            return productToReturn;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            dbContext.Products.Update(product);

            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return false;

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
