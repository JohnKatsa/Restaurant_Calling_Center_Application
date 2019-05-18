using FinalProjectMVC.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectMVC.Services
{
    public class OrderRetrieval
    {
        public async Task<Order> AddOrderAsync(List<Product> products)
        {
            using (var context = new ShopContext())
            {
                // create new order item
                Order o = new Order()
                {
                    Cost = products.Sum(p => p.Cost),
                    Products = products.ForEach(p => new OrderedProduct()
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Category = p.Category,
                        Cost = p.Cost,
                        Quantity = 1
                    }).,
                    PurchaseDate = DateTime.Now,
                    ExposeId = Guid.NewGuid()
                };

                // add order to database
                await context.Orders.AddAsync(o);
                await context.SaveChangesAsync();

                // return order to display
                return o;
            }
        }

        public async Task<Order> GetByExposeIdAsync(Guid Id)
        {
            using (var context = new ShopContext())
            {
                return await context.Orders.Where(o => o.ExposeId == Id).Include(o => o.Products).FirstOrDefaultAsync();
            }
        }
    }
}
