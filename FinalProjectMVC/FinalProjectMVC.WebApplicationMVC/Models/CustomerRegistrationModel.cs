using FinalProjectMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.WebApplicationMVC.Models
{
    public class CustomerRegistrationModel
    {
        public Customer Customer { get; set; }
        public Boolean IsOld { get; set; }

        public CustomerRegistrationModel()
        {
            Customer = new Customer();
            IsOld = false;
        }
    }
}
