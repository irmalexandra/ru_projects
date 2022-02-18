using System.Collections.Generic;
using AutoMapper.Configuration;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Services.Interfaces;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IQueueService _queueService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IPaymentService _paymentService;
        private readonly string _routingKey;

        public OrderService(IOrderRepository orderRepository, IQueueService queueService, IConfiguration configuration, IShoppingCartRepository shoppingCartRepository, IPaymentService paymentService)
        {
            _orderRepository = orderRepository;
            _queueService = queueService;
            _shoppingCartRepository = shoppingCartRepository;
            _paymentService = paymentService;
            _routingKey = configuration.GetSection("MessageBroker").GetSection("RoutingKey").Value;
        }

        public IEnumerable<OrderDto> GetOrders(string email)
        {
            return _orderRepository.GetOrders(email);
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {
            var orderDto = _orderRepository.CreateNewOrder(email, order);
            _queueService.PublishMessage(_routingKey, orderDto);
            _shoppingCartRepository.DeleteCart(email);
            return orderDto;
        }
    }
}