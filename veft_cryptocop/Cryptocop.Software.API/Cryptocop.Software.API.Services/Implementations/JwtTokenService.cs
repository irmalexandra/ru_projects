using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {

        private readonly ITokenRepository _tokenRepository;

        public JwtTokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            return _tokenRepository.IsTokenBlacklisted(tokenId);
        }
    }
}