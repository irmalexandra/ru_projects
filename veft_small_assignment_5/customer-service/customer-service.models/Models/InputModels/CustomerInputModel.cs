using System.ComponentModel.DataAnnotations;

namespace customer_service.Models.InputModels
{
    public class CustomerInputModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}