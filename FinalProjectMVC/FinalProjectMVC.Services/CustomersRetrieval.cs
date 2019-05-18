using FinalProjectMVC.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Services
{
    public class CustomersRetrieval
    {
        public async Task<List<Customer>> GetCustomersByNameAsync(string regex)
        {
            // make a disposable connection
            using (var context = new ShopContext())
            {
                var x = await context.Customers.Where(c => (c.FirstName + c.LastName).Contains(regex)).ToListAsync();
                // get new customers with regex in name
                return x;
            }
        }

        public async Task<Boolean> AddCustomerAsync(Customer customer)
        {
            using (var context = new ShopContext())
            {
                //var y = context.Customers.ToList();

                // if customer already exists, return error
                if (context.Customers.Where(c => c.PhoneNumber == customer.PhoneNumber).FirstOrDefault() != null)
                    return false;

                await context.Customers.AddAsync(new Customer()
                {
                    FirstName = customer.FirstName,
                    MiddleName = customer.MiddleName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    ExposeId = customer.ExposeId
                });
                await context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<Boolean> AddOldCustomerAsync(OldCustomer customer)
        {
            using (var context = new ShopContext())
            {
                //var y = context.Customers.ToList();

                // if customer already exists, return error
                if (context.OldCustomers.Where(c => c.OldDbId == customer.OldDbId).FirstOrDefault() != null)
                    return false;

                await context.OldCustomers.AddAsync(new OldCustomer()
                {
                    FirstName = customer.FirstName,
                    MiddleName = customer.MiddleName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    OldDbId = customer.OldDbId,
                    ExposeId = customer.ExposeId
                });
                await context.SaveChangesAsync();
            }
            return true;
        }

        public Boolean ExistsOldInDb(int oldid)
        {
            using(var context = new ShopContext())
            {
                return context.OldCustomers.Where(o => o.OldDbId == oldid).FirstOrDefault() != null;
            }
        }

        public async Task<Order> AddOrderToCustomerAsync(string[] buy, Guid Id)
        {
            using(var context = new ShopContext())
            {
                // Check to make good update
                Customer customer = await context.Customers.Where(c => c.ExposeId == Id).FirstOrDefaultAsync();

                List<Product> products = new List<Product>();
                foreach (var id in buy)
                    products.Add(await context.Products.Where(p => p.Id.ToString() == id).FirstOrDefaultAsync());

                if (customer == null)
                    return null;

                // if it is null assign new empty list
                if (customer.Orders == null)
                    customer.Orders = new List<Order>();

                Order order = new Order()
                {
                    Cost = products.Sum(p => p.Cost),
                    Products = products,
                    PurchaseDate = DateTime.Now,
                    ExposeId = Guid.NewGuid()
                };
                customer.Orders.Add(order);

                await context.SaveChangesAsync();

                return order;
            }
        }
    }
}
