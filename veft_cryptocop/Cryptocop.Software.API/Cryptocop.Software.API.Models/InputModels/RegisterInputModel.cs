using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Software.API.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [MinLength(3)] public string FullName { get; set; }
        [Required] [MinLength(8)] [DataType(DataType.Password)] public string Password { get; set; }
        [Required] [MinLength(8)] [DataType(DataType.Password)] [Compare("Password")] public string PasswordConfirmation { get; set; }
    }
}