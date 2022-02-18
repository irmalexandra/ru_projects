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
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IAddressService _addressService;
        private readonly ClaimsAuthorizationHelper _claimsAuthorizationHelper;

        public AddressController(IAddressService addressService, ClaimsAuthorizationHelper claimsAuthorizationHelper)
        {
            _addressService = addressService;
            _claimsAuthorizationHelper = claimsAuthorizationHelper;
        }

        [Route("", Name = "GetAllAddresses")]
        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            return Ok(_addressService.GetAllAddresses(userEmail));
        }
        
        [Route("", Name = "AddAddress")]
        [HttpPost]
        public IActionResult AddAddress([FromBody] AddressInputModel address)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException(ModelState.RetrieveErrorString());}
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            var addressDto = _addressService.AddAddress(userEmail, address);
            return CreatedAtRoute("AddAddress", new {id = addressDto.Id}, addressDto);
        }
        
        [Route("{addressId}", Name = "DeleteAddress")]
        [HttpDelete]
        public IActionResult DeleteAddress(int addressId)
        {
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            _addressService.DeleteAddress(userEmail, addressId);
            return NoContent();
        }
    }
}