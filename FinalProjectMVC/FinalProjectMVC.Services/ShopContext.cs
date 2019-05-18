using FinalProjectMVC.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Services
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        public ShopContext() { } 

        public DbSet<Customer> Customers { get; set; }
        public DbSet<OldCustomer> OldCustomers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ShopNewDB2;Trusted_Connection=True;");
        }
    }
}
