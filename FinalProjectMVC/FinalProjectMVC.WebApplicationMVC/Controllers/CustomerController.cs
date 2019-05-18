using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FinalProjectMVC.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FinalProjectMVC.Services;
using FinalProjectMVC.WebApplicationMVC.Models;

namespace FinalProjectMVC.WebApplicationMVC.Controllers
{
    [Route("[Controller]")]
    public class CustomerController : Controller
    {
        private readonly HttpClient client;
        private readonly String webApiUri;
        private readonly ShopRepository shopRepository;

        public CustomerController()
        {
            // web api location
            webApiUri = "https://localhost:44325/customer";
            client = new HttpClient();
            shopRepository = new ShopRepository();
        }

        [HttpGet]
        // This action is used to display customers from web api, to the user
        public async Task<IActionResult> Index(string search = "")
        {
            string response;
            CustomerListViewModel customerListModel = new CustomerListViewModel();

            try
            {
                // get old customers
                response = await client.GetStringAsync(webApiUri + "?regex=" + search);
            }
            catch (Exception)
            {
                // load only new customers if http request fails
                customerListModel.NewCustomers = await shopRepository.GetCustomersByNameAsync(search);
                return View(customerListModel);
            }

            // List of old customers and new customers
            customerListModel.OldCustomers = JsonConvert.DeserializeObject<List<Customer>>(response).Select(c => new OldCustomer()
            {
                FirstName = c.FirstName,
                MiddleName = c.MiddleName,
                LastName = c.LastName,
                OldDbId = c.Id,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                City = c.City,
                PostalCode = c.PostalCode
            }).Where(o => !shopRepository.ExistsOldInDb(o.OldDbId)).ToList(); // exclude old tha are already put in

            customerListModel.NewCustomers = await shopRepository.GetCustomersByNameAsync(search);
            return View(customerListModel);
        }

        [HttpGet]
        [Route("Register")]
        public async Task<IActionResult> Register(int Id = -1)
        {
            CustomerRegistrationModel customerRegistrationModel = new CustomerRegistrationModel();

            // When we want to diplay a new empty form (new customer)
            if (Id == -1)
                customerRegistrationModel.IsOld = false;
            else
            {
                string response = await client.GetStringAsync(webApiUri + "/" + Id);
                Customer customer = JsonConvert.DeserializeObject<Customer>(response);

                customerRegistrationModel.Customer = customer;
                customerRegistrationModel.IsOld = true;
            }
            return View(customerRegistrationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public async Task<IActionResult> Register(CustomerRegistrationModel customerRegistrationModel)
        {
            // add new customer to database
            Boolean success;
            Guid newGuid = Guid.NewGuid();

            if (customerRegistrationModel.IsOld)
                success = await shopRepository.AddOldCustomerAsync(new OldCustomer()
                {
                    FirstName = customerRegistrationModel.Customer.FirstName,
                    MiddleName = customerRegistrationModel.Customer.MiddleName,
                    LastName = customerRegistrationModel.Customer.LastName,
                    OldDbId = customerRegistrationModel.Customer.Id,
                    PhoneNumber = customerRegistrationModel.Customer.PhoneNumber,
                    Address = customerRegistrationModel.Customer.Address,
                    City = customerRegistrationModel.Customer.City,
                    PostalCode = customerRegistrationModel.Customer.PostalCode,
                    ExposeId = newGuid
                });
            else
            {
                customerRegistrationModel.Customer.ExposeId = newGuid;
                success = await shopRepository.AddCustomerAsync(customerRegistrationModel.Customer);
            }

            if (success)
                return RedirectToAction(nameof(Index), "Order", new { Id = newGuid});
            else
                return View(customerRegistrationModel);
        }
    }
}