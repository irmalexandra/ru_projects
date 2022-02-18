using Cryptocop.Software.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Software.API.Repositories.Contexts
{
    public class CryptocopDbContext : DbContext
    {
        public CryptocopDbContext(DbContextOptions<CryptocopDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(address => address.User)
                .WithMany(user => user.Addresses);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(cart => cart.User)
                .WithOne(user => user.ShoppingCart);

            modelBuilder.Entity<PaymentCard>()
                .HasOne(card => card.User)
                .WithMany(user => user.PaymentCards);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.User)
                .WithMany(user => user.Orders);
            
            modelBuilder.Entity<OrderItem>()
                .HasOne(item => item.Order)
                .WithMany(order => order.OrderItems);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(item => item.ShoppingCart)
                .WithMany(cart => cart.ShoppingCartItems);
        }
        
        // Setup DbSets which function as our tables
        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentCard> PaymentCards { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}