using Cryptocop.Software.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Cryptocop.Software.API.Services.Interfaces;
using Exterminator.WebApi.Extensions;
using TechnicalRadiation.Models.Exceptions;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly ClaimsAuthorizationHelper _claimsAuthorizationHelper;
        
        public AccountController(IAccountService accountService, ITokenService tokenService, ClaimsAuthorizationHelper claimsAuthorizationHelper)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _claimsAuthorizationHelper = claimsAuthorizationHelper;
        }

        [AllowAnonymous]
        [Route("register", Name = "RegisterUser")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterInputModel newUser)
        {
            
            if (!ModelState.IsValid) { throw new ModelFormatException(ModelState.RetrieveErrorString()); }
            var userDto = _accountService.CreateUser(newUser);
            return CreatedAtRoute("RegisterUser",new {id = userDto.Id}, userDto);
        }

        [AllowAnonymous]
        [Route("signin", Name = "SignInUser")]
        [HttpPost]
        public IActionResult SignInUser([FromBody] LoginInputModel login)
        {
            var user = _accountService.AuthenticateUser(login);
            if (user == null) { throw new InvalidLoginException(); }
            
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(token);
        }
        
        [Route("signout", Name = "SignOutUser")]
        [HttpGet]
        public IActionResult SignOutUser()
        {
            int.TryParse(_claimsAuthorizationHelper.GetAuthorizedUserTokenId(User), out var tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }
        
    }
    

    
    
}