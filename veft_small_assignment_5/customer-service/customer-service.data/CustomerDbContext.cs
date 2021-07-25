using customer_service.models.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace customer_service.data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) {}
        public DbSet<Customer> Customers { get; set; }
    }
}