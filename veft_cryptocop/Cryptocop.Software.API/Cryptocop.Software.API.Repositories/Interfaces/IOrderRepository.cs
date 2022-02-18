using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<OrderDto> GetOrders(string email);
        OrderDto CreateNewOrder(string email, OrderInputModel order);
    }
}