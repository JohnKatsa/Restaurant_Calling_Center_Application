using FinalProject.Domain;
using FinalProject.Interfaces.IDataRetrieval;
using FinalProject.Services.DataRetrieval;
using FinalProject.WebApplicationREST.Domain;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FinalProject.WebApplicationREST.Services
{
    public class CustomerRetrieval : IDataRetrievalByRegex<Customer>
    {
        /* Service Entities */
        private EmployeeRetrieval employeeRetrieval;
        private PersonRetrieval personRetrieval;
        private EmailAddressRetrieval emailAddressRetrieval;
        private PhoneNumberRetrieval phoneNumberRetrieval;
        private AddressRetrieval addressRetrieval;

        public CustomerRetrieval()
        {
            employeeRetrieval = new EmployeeRetrieval();
            personRetrieval = new PersonRetrieval();
            emailAddressRetrieval = new EmailAddressRetrieval();
            phoneNumberRetrieval = new PhoneNumberRetrieval();
            addressRetrieval = new AddressRetrieval();
        }

        public async Task<IEnumerable<Customer>> GetByNameAsync(string regex)
        {
            // get person list with regex in full name
            IEnumerable<Person> person = await personRetrieval.GetByNameAsync(regex);

            // get employee list with regex in full name
            List<Employee> employees = new List<Employee>();
            foreach (var p in person)
            {
                employees.Add(await employeeRetrieval.GetByIdAsync(p.BusinessEntityId));
            }

            // get email address list with regex in full name
            List<EmailAddress> emailAddress = new List<EmailAddress>();
            foreach (var p in person)
            {
                emailAddress.Add(await emailAddressRetrieval.GetByIdAsync(p.BusinessEntityId));
            }

            // get phone number list with regex in full name
            List<PersonPhone> phoneNumbers = new List<PersonPhone>();
            foreach (var p in person)
            {
                phoneNumbers.Add(await phoneNumberRetrieval.GetByIdAsync(p.BusinessEntityId));
            }

            // get address list with regex in full name
            List<Address> addresses = new List<Address>();
            foreach (var p in person)
            {
                addresses.Add(await addressRetrieval.GetByIdAsync(p.BusinessEntityId));
            }

            // if the size differs, something bad has happened
            if (employees.Count() != person.Count()
            || employees.Count() != emailAddress.Count()
            || employees.Count() != phoneNumbers.Count())
                throw new Exception("Bad");

            List<Customer> customers = new List<Customer>();
            for(int i = 0; i < employees.Count(); i++)
            {
                // customer = AdventureWorks2017.employee
                // we also want to be employee, not just person
                if (employees.ToList()[i] != null)
                {
                    customers.Add(new Customer()
                    {
                        BusinessEntityId = employees.ToList()[i].BusinessEntityId,
                        FirstName = person.ToList()[i].FirstName,
                        MiddleName = person.ToList()[i].MiddleName,
                        LastName = person.ToList()[i].LastName,
                        EmailAddressString = emailAddress.ToList()[i].EmailAddress1,
                        PhoneNumber = phoneNumbers.ToList()[i].PhoneNumber,
                        Address = addresses.ToList()[i].AddressLine1 + addresses.ToList()[i].AddressLine2,
                        City = addresses.ToList()[i].City,
                        PostalCode = addresses.ToList()[i].PostalCode
                    });
                }
            }

            return customers;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            // get phone by phone number
            PersonPhone phoneNumber = await phoneNumberRetrieval.GetByIdAsync(id);

            // get person by id
            Person person = await personRetrieval.GetByIdAsync(phoneNumber.BusinessEntityId);

            // get employee by id
            Employee employee = await employeeRetrieval.GetByIdAsync(phoneNumber.BusinessEntityId);

            // get email address by id
            EmailAddress emailAddress = await emailAddressRetrieval.GetByIdAsync(phoneNumber.BusinessEntityId);

            // get address by id
            Address address = await addressRetrieval.GetByIdAsync(phoneNumber.BusinessEntityId);

            // form new customer object
            Customer customer = new Customer()
            {
                BusinessEntityId = employee.BusinessEntityId,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                EmailAddressString = emailAddress.EmailAddress1,
                PhoneNumber = phoneNumber.PhoneNumber,
                Address = address.AddressLine1 + address.AddressLine2,
                City = address.City,
                PostalCode = address.PostalCode
            };

            return customer;
        }
    }
}
