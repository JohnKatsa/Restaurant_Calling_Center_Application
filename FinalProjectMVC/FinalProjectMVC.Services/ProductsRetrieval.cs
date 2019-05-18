using FinalProjectMVC.Domain;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Services
{
    public class ProductsRetrieval
    {
        public IEnumerable<Product> Products;

        public async Task<List<Product>> GetProductsAsync()
        {
            // make a disposable connection
            using (var context = new ShopContext())
            {
                return await context.Products.Include(Products => Products.Category).ToListAsync();
            }
        }

        public async Task<List<Product>> GetProductsByIdsAsync(string[] ids)
        {
            List<Product> products = new List<Product>();

            using (var context = new ShopContext())
            {
                foreach (var id in ids)
                    products.Add(await context.Products.Where(p => p.Id.ToString() == id).FirstOrDefaultAsync());
            }

            return products;
        }
    }
}
