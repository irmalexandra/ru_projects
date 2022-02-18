using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Repositories.Interfaces;
using TechnicalRadiation.Models.Exceptions;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly IMapper _mapper;

        public ShoppingCartRepository(CryptocopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }
            
            var shoppingCartItemDtos = _dbContext.ShoppingCartItems.Where(item => item.ShoppingCart.User.Email == email)
                ?.Select(item => _mapper.Map<ShoppingCartItemDto>(item));
            return shoppingCartItemDtos;
        }

        public ShoppingCartItemDto AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItem, float priceInUsd)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }

            var currentShoppingCart = _dbContext.ShoppingCarts.FirstOrDefault(cart => cart.User.Email == email);
            if (currentShoppingCart == null)
            {
                currentShoppingCart = new ShoppingCart();
                currentShoppingCart.UserId = currentUser.Id;
                _dbContext.ShoppingCarts.Add(currentShoppingCart);
                _dbContext.SaveChanges();
            }
            
            var shoppingCartItemEntity = _mapper.Map<ShoppingCartItem>(shoppingCartItem);
            shoppingCartItemEntity.UnitPrice = priceInUsd;
            
            shoppingCartItemEntity.ShoppingCartId =
                _dbContext.ShoppingCarts.FirstOrDefault(cart => cart.User.Email == email).Id;

            _dbContext.ShoppingCartItems.Add(shoppingCartItemEntity);
            _dbContext.SaveChanges();

            return _mapper.Map<ShoppingCartItemDto>(shoppingCartItemEntity);
        }

        public void RemoveCartItem(string email, int id)
        {
            _dbContext.ShoppingCartItems.Remove(
                _dbContext.ShoppingCartItems.FirstOrDefault(item =>
                    item.Id == id && item.ShoppingCart.User.Email == email)!);
            _dbContext.SaveChanges();
        }

        public void UpdateCartItemQuantity(string email, int id, float quantity)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }
            
            var cartItem = _dbContext.ShoppingCartItems.FirstOrDefault(item =>
                item.Id == id && item.ShoppingCart.User.Email == email)!;
            if (cartItem == null) { throw new ResourceNotFoundException(); }
            
            cartItem.Quantity = quantity;
            _dbContext.SaveChanges();
        }

        public void ClearCart(string email)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }

            var itemsToRemove = _dbContext.ShoppingCartItems.Where(item => item.ShoppingCart.User.Email == email).ToList();
            _dbContext.ShoppingCartItems.RemoveRange(itemsToRemove);
            _dbContext.SaveChanges();
        }

        public void DeleteCart(string email)
        {
            ClearCart(email);
            _dbContext.ShoppingCarts.Remove(_dbContext.ShoppingCarts.FirstOrDefault(cart => cart.User.Email == email));
            _dbContext.SaveChanges();
        }
    }
}