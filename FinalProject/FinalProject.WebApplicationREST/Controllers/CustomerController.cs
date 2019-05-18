using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FinalProject.WebApplicationREST.Services;

namespace FinalProject.WebApplicationREST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRetrieval customersRetrieval;

        public CustomerController()
        {
            customersRetrieval = new CustomerRetrieval();
        }

        // GET [Controller]/
        // get list of customers with regex in name
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get(string regex = "")
        {
            return Ok(await customersRetrieval.GetByNameAsync(regex));
        }

        // GET [Controller]/{Id}
        // get directly customer by his Id
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<IEnumerable<string>>> GetById(int Id)
        {
            return Ok(await customersRetrieval.GetByIdAsync(Id));
        }
    }
}
