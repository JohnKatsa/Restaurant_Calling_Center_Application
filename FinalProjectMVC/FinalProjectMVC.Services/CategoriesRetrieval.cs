using FinalProjectMVC.Domain;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Services
{
    public class CategoriesRetrieval
    {
        public async Task<List<Category>> GetCategoriesAsync()
        {
            // make a disposable connection
            using (var context = new ShopContext())
            {
                return await context.Categories.ToListAsync();
            }
        }
    }
}
