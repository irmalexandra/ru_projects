using customer_service.Models.InputModels;
using customer_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace customer_service.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        [HttpGet]
        [Route("{customerId}")]
        public IActionResult GetCustomerById(int customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null) { return NotFound(); }
            return Ok(customer);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateCustomer([FromBody] CustomerInputModel customer)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            return Ok(_customerService.CreateCustomer(customer));
        }
    }
}
