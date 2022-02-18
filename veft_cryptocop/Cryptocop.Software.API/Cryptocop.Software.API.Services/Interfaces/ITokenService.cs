using Cryptocop.Software.API.Models.Dtos;

namespace Cryptocop.Software.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserDto user);
    }
}