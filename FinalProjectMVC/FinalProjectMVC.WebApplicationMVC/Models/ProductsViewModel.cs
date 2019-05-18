using FinalProjectMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.WebApplicationMVC.Models
{
    public class ProductsViewModel
    {
        public Guid Id { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
