using System.Linq;
using Cryptocop.Software.API.Helpers;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Exterminator.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Exceptions;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly ClaimsAuthorizationHelper _claimsAuthorizationHelper;

        public OrderController(IOrderService orderService, ClaimsAuthorizationHelper claimsAuthorizationHelper)
        {
            _orderService = orderService;
            _claimsAuthorizationHelper = claimsAuthorizationHelper;
        }

        [Route("", Name = "GetAllOrders")]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            return Ok(_orderService.GetOrders(userEmail));
        }
        
        [Route("", Name = "AddOrder")]
        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderInputModel order)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException(ModelState.RetrieveErrorString()); }
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            var orderDto = _orderService.CreateNewOrder(userEmail, order);
 
            return Created("AddOrder", orderDto);
        }
        
    }
}