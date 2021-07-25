using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api_gateway.Models.InputModels
{
    public class CustomerInputModel
    {
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [Required]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}