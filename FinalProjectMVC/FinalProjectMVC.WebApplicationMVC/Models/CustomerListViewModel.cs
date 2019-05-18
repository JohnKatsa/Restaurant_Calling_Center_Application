using FinalProjectMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.WebApplicationMVC.Models
{
    public class CustomerListViewModel
    {
        public List<Customer> NewCustomers { get; set; }
        public List<OldCustomer> OldCustomers { get; set; }
    }
}
