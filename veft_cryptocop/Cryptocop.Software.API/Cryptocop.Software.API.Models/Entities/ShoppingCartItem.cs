using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Software.API.Models.Entities
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string ProductIdentifier { get; set; }
        public float Quantity { get; set; }
        public float UnitPrice { get; set; }
        
        // Navigation properties
        
        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}