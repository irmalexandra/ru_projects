using System.Collections.Generic;
using customer_service.Models.Dtos;
using customer_service.Models.InputModels;

namespace customer_service.Services
{
    public interface ICustomerService
    {
         CustomerDto CreateCustomer(CustomerInputModel customer);
         IEnumerable<CustomerDto> GetAllCustomers();
         CustomerDto GetCustomerById(int customerId);
    }
}