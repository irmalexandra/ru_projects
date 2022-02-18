﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Services.Interfaces
{
    public interface IShoppingCartService
    {
        IEnumerable<ShoppingCartItemDto> GetCartItems(string email);
        Task<ShoppingCartItemDto> AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItemItem);
        void RemoveCartItem(string email, int id);
        void UpdateCartItemQuantity(string email, int id, float quantity);
        void ClearCart(string email);
    }
}