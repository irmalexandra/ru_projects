using System;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderRepository(CryptocopDbContext dbContext, IMapper mapper, IShoppingCartRepository shoppingCartRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
        }
        
        public IEnumerable<OrderDto> GetOrders(string email)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new Exception("User not found"); }

            var allOrders = _dbContext.Orders.Where(order => order.User.Email == email)
                .Select(order => _mapper.Map<OrderDto>(order)).ToList();

            foreach (var order in allOrders)
            {
                order.OrderItems = _dbContext.OrderItems.Where(item => item.OrderId == order.Id)
                    .Select(item => _mapper.Map<OrderItemDto>(item)).ToList();
            }
            
            return allOrders;
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }

            var currentAddress = _dbContext.Addresses.FirstOrDefault(address =>
                address.Id == order.AddressId && address.UserId == currentUser.Id);
            if (currentAddress == null) { throw new ResourceNotFoundException("Address not found"); }
            
            var currentPaymentCard = _dbContext.PaymentCards.FirstOrDefault(card =>
                card.Id == order.PaymentCardId && card.UserId == currentUser.Id);
            if (currentPaymentCard == null) { throw new ResourceNotFoundException("Paymentcard not found"); }
            
            var orderEntity = _mapper.Map<Order>(currentPaymentCard); // paymentCard is mapped so automapper can mask cardnumber
            orderEntity.FullName = currentUser.FullName;
            orderEntity.Email = currentUser.Email;
            orderEntity.StreetName = currentAddress.StreetName;
            orderEntity.HouseNumber = currentAddress.HouseNumber;
            orderEntity.ZipCode = currentAddress.ZipCode;
            orderEntity.Country = currentAddress.Country;
            orderEntity.City = currentAddress.City;
            orderEntity.OrderDate = DateTime.Now;
            
            _dbContext.Orders.Add(orderEntity);
            
            _dbContext.SaveChanges();
            
            var orderId = orderEntity.Id;
            var shoppingCartItems = _shoppingCartRepository.GetCartItems(email).ToList();
            if (shoppingCartItems.Count == 0) { throw new ResourceNotFoundException("Your cart is empty."); }
            
            var orderDto = _mapper.Map<OrderDto>(orderEntity);

            foreach (var item in shoppingCartItems)
            {
                var orderItem = _mapper.Map<OrderItem>(item);
                orderItem.OrderId = orderEntity.Id;
                
                _dbContext.OrderItems.Add(orderItem);
                _dbContext.SaveChanges(); // Saves after every item so that the id is updated.
                
                orderEntity.TotalPrice += orderItem.TotalPrice;
                orderDto.OrderItems.Add(_mapper.Map<OrderItemDto>(orderItem));
            }

            _dbContext.SaveChanges();
            orderDto.TotalPrice = orderEntity.TotalPrice;
            orderDto.CreditCard = currentPaymentCard.CardNumber;
            
            return orderDto;
        }
    }
}