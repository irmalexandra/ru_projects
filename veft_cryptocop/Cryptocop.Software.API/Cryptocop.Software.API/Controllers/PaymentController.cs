using Cryptocop.Software.API.Helpers;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Exterminator.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Exceptions;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ClaimsAuthorizationHelper _claimsAuthorizationHelper;
            

        public PaymentController(IPaymentService paymentService, ClaimsAuthorizationHelper claimsAuthorizationHelper)
        {
            _paymentService = paymentService;
            _claimsAuthorizationHelper = claimsAuthorizationHelper;
        }

        [Route("", Name = "GetAllPayments")]
        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            return Ok(_paymentService.GetStoredPaymentCards(userEmail));
        }
        
        [Route("", Name = "AddPaymentCard")]
        [HttpPost]
        public IActionResult AddPaymentCard(PaymentCardInputModel paymentCardInputModel)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException(ModelState.RetrieveErrorString());}
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            var paymentCardDto = _paymentService.AddPaymentCard(userEmail, paymentCardInputModel);
            return Created("AddPaymentCard", paymentCardDto);
        }
    }
}