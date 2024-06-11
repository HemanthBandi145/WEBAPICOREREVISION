using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIRevision.Data;
using WebAPIRevision.DTO;
using WebAPIRevision.Models;

namespace WebAPIRevision.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        //private readonly ECommerceContext context;

        //public CustomersController(ECommerceContext context)
        //{
        //    this.context = context;
        //}

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            // var data = await context.Customers.ToListAsync();
            var data = await customerRepository.GetAllCustomersAsync();
            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var data = await customerRepository.GetCustomerByIdAsync(id);
            return Ok(data);
        } 

        [HttpPost]
        public async Task<ActionResult> UpdateCustomerDetails([FromBody]CustomerDTO customer)
        {
            await customerRepository.UpdateCustomer(customer);
            return Ok("Customer Updated Successfully !!!! ");
        }


    }
}
