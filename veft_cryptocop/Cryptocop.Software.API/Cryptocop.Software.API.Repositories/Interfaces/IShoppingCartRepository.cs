﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        IEnumerable<ShoppingCartItemDto> GetCartItems(string email);
        ShoppingCartItemDto AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItemItem, float priceInUsd);
        void RemoveCartItem(string email, int id);
        void UpdateCartItemQuantity(string email, int id, float quantity);
        void ClearCart(string email);
        void DeleteCart(string email);
    }
}