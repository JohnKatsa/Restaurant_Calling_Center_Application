using System;

namespace FinalProject.WebApplicationREST.Domain
{
    public class Customer
    {
        public int BusinessEntityId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddressString { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
