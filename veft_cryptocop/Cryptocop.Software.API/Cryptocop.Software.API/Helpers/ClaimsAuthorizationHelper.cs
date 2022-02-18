using Cryptocop.Software.API.Models.Entities;
using System.Linq;
using System.Security.Claims;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Helpers
{
    public class ClaimsAuthorizationHelper
    {
        public string GetAuthorizedUserEmail(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value;
        }

        public string GetAuthorizedUserTokenId(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == "tokenId")?.Value;
        }
        
    }
}