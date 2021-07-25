using System;
using System.Net.Http;
using System.Threading.Tasks;
using api_gateway.Exceptions;
using api_gateway.Models.Dtos;
using api_gateway.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace api_gateway.Controllers
{
    [Route("api")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration Configuration;
        private string CustomerServiceHost;
        private string OrderServiceHost;

        public GatewayController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            Configuration = configuration;
            CustomerServiceHost = Configuration.GetSection("Communication").GetSection("CustomerService").Value;
            OrderServiceHost = Configuration.GetSection("Communication").GetSection("OrderService").Value;
        }

        [HttpGet]
        [Route("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {

            var customers = await IssueHttpRequest($"{CustomerServiceHost}/api/customers", HttpMethod.Get, null);
            return Ok(customers);
        }

        [HttpPost]
        [Route("customers")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerInputModel customer)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var newCustomer = JsonConvert.DeserializeObject<CustomerDto>(await IssueHttpRequest($"{CustomerServiceHost}/api/customers", HttpMethod.Post, customer));
            return CreatedAtRoute("GetCustomerById", new { customerId = newCustomer.Id }, null);
        }

        [HttpGet]
        [Route("customers/{customerId}", Name="GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await IssueHttpRequest($"{CustomerServiceHost}/api/customers/{customerId}", HttpMethod.Get, null);
            return Ok(customer);
        }

        [HttpGet]
        [Route("customers/{customerId}/orders")]
        public async Task<IActionResult> GetAllCustomerOrders(int customerId)
        {
            var orders = await IssueHttpRequest($"{OrderServiceHost}/api/customers/{customerId}/orders", HttpMethod.Get, null);
            return Ok(orders);
        }

        [HttpPost]
        [Route("customers/{customerId}/orders")]
        public async Task<IActionResult> CreateOrder(int customerId, [FromBody] OrderInputModel order)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            order.CustomerId = customerId;
            await IssueHttpRequest($"{OrderServiceHost}/api/orders", HttpMethod.Post, order);
            return StatusCode(201);
        }

        private async Task<string> IssueHttpRequest(string url, HttpMethod method, object data)
        {
            var client = _clientFactory.CreateClient();

            HttpResponseMessage response;

            if (method == HttpMethod.Post)
            {
                response = await client.PostAsJsonAsync(url, data);
            }
            else
            {
                response = await client.GetAsync(url);
            }

            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound: throw new NotFoundException();
                    case HttpStatusCode.BadRequest: throw new BadRequestException();
                    default: throw new Exception();
                }
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
