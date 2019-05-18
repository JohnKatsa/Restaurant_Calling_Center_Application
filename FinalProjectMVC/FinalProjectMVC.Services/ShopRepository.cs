using FinalProjectMVC.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectMVC.Services
{
    public class ShopRepository
    {
        // Get all customers with regex in their full name.
        public async Task<List<Customer>> GetCustomersByNameAsync(string regex)
        {
            using (var context = new ShopContext())
            {
                return await context.Customers.Where(c => (c.FirstName + c.LastName).Contains(regex)).ToListAsync();
            }
        }

        // Add new customer (not existing in old data base)
        public async Task<Boolean> AddCustomerAsync(Customer customer)
        {
            using (var context = new ShopContext())
            {
                // if customer already exists, return error
                if (context.Customers.Where(c => c.PhoneNumber == customer.PhoneNumber).FirstOrDefault() != null)
                    return false;

                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();
            }
            return true;
        }

        // Add old customer in current data base
        public async Task<Boolean> AddOldCustomerAsync(OldCustomer customer)
        {
            using (var context = new ShopContext())
            {
                // if customer already exists, return error
                if (context.OldCustomers.Where(c => c.OldDbId == customer.OldDbId).FirstOrDefault() != null)
                    return false;

                await context.OldCustomers.AddAsync(customer);
                await context.SaveChangesAsync();
            }
            return true;
        }

        // Check if old customer already registered in new data base
        public Boolean ExistsOldInDb(int oldid)
        {
            using (var context = new ShopContext())
            {
                return context.OldCustomers.Where(o => o.OldDbId == oldid).FirstOrDefault() != null;
            }
        }

        // Add given order to given customer's history
        public async Task<Order> AddOrderToCustomerAsync(string[] buy, string[] buyAmount, Guid Id)
        {
            if (buy.Count() != buyAmount.Count())
                throw new Exception("Different number of bought items and bought amounts");

            using (var context = new ShopContext())
            {
                // Get given customer. If he doesn't exist, return null
                Customer customer = await context.Customers.Where(c => c.ExposeId == Id).FirstOrDefaultAsync();
                if (customer == null)
                    return null;

                // Convert shop products to ordered products
                List<OrderedProduct> products = new List<OrderedProduct>();
                for (int i = 0; i < buy.Count(); i++)
                    products.Add(
                        MoveProductToOrderedProductWithQuantity(
                            await context.Products.Where(p => p.Id.ToString() == buy[i]).FirstOrDefaultAsync(),
                            Int32.Parse(buyAmount[i])
                        )
                    );

                // if it is null assign new empty list
                if (customer.Orders == null)
                    customer.Orders = new List<Order>();

                Order order = new Order()
                {
                    Cost = products.Sum(p => p.Cost * p.Quantity),
                    Products = products,
                    PurchaseDate = DateTime.Now,
                    ExposeId = Guid.NewGuid()
                };
                customer.Orders.Add(order);

                await context.SaveChangesAsync();

                return order;
            }
        }

        // @Helper Fuction to convert product to ordered product AND calculate cost for each product
        public OrderedProduct MoveProductToOrderedProductWithQuantity(Product product, int quantity)
        {
            return new OrderedProduct()
            {
                Name = product.Name,
                Category = product.Category,
                Description = product.Description,
                Cost = product.Cost,
                Quantity = quantity
            };
        }

        // Get all product categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            using (var context = new ShopContext())
            {
                return await context.Categories.ToListAsync();
            }
        }

        // Get order using its auto-generated guid
        public async Task<Order> GetOrderByExposeIdAsync(Guid Id)
        {
            using (var context = new ShopContext())
            {
                return await context.Orders.Where(o => o.ExposeId == Id).Include(o => o.Products).FirstOrDefaultAsync();
            }
        }

        // Get all products
        public async Task<List<Product>> GetProductsAsync()
        {
            using (var context = new ShopContext())
            {
                return await context.Products.Include(Products => Products.Category).ToListAsync();
            }
        }
    }
}
