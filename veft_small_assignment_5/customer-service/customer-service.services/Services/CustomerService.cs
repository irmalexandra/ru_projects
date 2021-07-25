using System;
using System.Collections.Generic;
using System.Linq;
using customer_service.data;
using customer_service.models.Models.Entities;
using customer_service.Models.Dtos;
using customer_service.Models.InputModels;
using customer_service.Publisher;
using Microsoft.Extensions.Configuration;

namespace customer_service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRabbitMqClient _mbClient;
        private readonly CustomerDbContext _dbContext;
        private readonly string _routingKey;

        public CustomerService(IRabbitMqClient mbClient, IConfiguration configuration, CustomerDbContext dbContext)
        {
            _mbClient = mbClient;
            _routingKey = configuration.GetSection("MessageBroker").GetSection("RoutingKey").Value;
            _dbContext = dbContext;
        }

        public CustomerDto CreateCustomer(CustomerInputModel customer)
        {
            var newCustomer = _dbContext.Customers.Add(new Customer {
                Name = customer.Name,
                Email = customer.Email,
                Password = customer.Password
            }).Entity;

            _dbContext.SaveChanges();

            var dto = new CustomerDto
            {
                Id = newCustomer.Id,
                Name = newCustomer.Name,
                Email = newCustomer.Email
            };

            _mbClient.PublishMessage(_routingKey, dto);

            return dto;
        }

        public IEnumerable<CustomerDto> GetAllCustomers() => _dbContext.Customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email
        });

        public CustomerDto GetCustomerById(int customerId)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null) { return null; }
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }
    }
}
