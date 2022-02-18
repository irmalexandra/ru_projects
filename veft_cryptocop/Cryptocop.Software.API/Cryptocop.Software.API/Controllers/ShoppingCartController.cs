using System.Threading.Tasks;
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
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ClaimsAuthorizationHelper _claimsAuthorizationHelper;
        
        public ShoppingCartController(IShoppingCartService shoppingCartService, ClaimsAuthorizationHelper claimsAuthorizationHelper)
        {
            _shoppingCartService = shoppingCartService;
            _claimsAuthorizationHelper = claimsAuthorizationHelper;
        }

        [Route("", Name = "GetShoppingCart")]
        [HttpGet]
        public IActionResult GetShoppingCart()
        {
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            return Ok(_shoppingCartService.GetCartItems(userEmail));
        }
            
        
        [Route("", Name = "AddItemToShoppingCart")]
        [HttpPost]
        public async Task<CreatedAtRouteResult> AddItemToShoppingCart([FromBody] ShoppingCartItemInputModel shoppingCartItemInputModel)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException(ModelState.RetrieveErrorString());}
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            var shoppingCartItemDto = await _shoppingCartService.AddCartItem(userEmail, shoppingCartItemInputModel);
            return CreatedAtRoute("AddItemToShoppingCart", new {id = shoppingCartItemDto.Id});
        }
        
        [Route("{cartId}", Name = "DeleteItemFromShoppingCart")]
        [HttpDelete]
        public IActionResult DeleteItemFromShoppingCart(int cartId)
        {
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            _shoppingCartService.RemoveCartItem(userEmail, cartId);
            return NoContent();
        }

        [Route("{cartItemId}", Name = "UpdateItemQuantityInShippingCart")]
        [HttpPatch]
        public IActionResult UpdateItemQuantityInShippingCart(int cartItemId, [FromBody] ShoppingCartItemInputModel quantityUpdate)
        {
            if (quantityUpdate.Quantity < 0.01) { throw new ModelFormatException(ModelState.RetrieveErrorString());}
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            _shoppingCartService.UpdateCartItemQuantity(userEmail, cartItemId, quantityUpdate.Quantity);
            return NoContent();
        }        
        
        [Route("", Name = "EmptyCart")]
        [HttpDelete]
        public IActionResult EmptyCart()
        {
            var userEmail = _claimsAuthorizationHelper.GetAuthorizedUserEmail(User);
            _shoppingCartService.ClearCart(userEmail);
            return NoContent();
        }
    }
}