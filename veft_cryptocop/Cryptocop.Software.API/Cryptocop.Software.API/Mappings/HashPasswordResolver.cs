using AutoMapper;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Helpers;

namespace Cryptocop.Software.API.Mappings
{
    public class HashPasswordResolver : IValueResolver<RegisterInputModel, User, string>
    {
        public string Resolve(RegisterInputModel source, User destination, string destMember, ResolutionContext context)
        {
            return $"{destination.HashedPassword}{HashingHelper.HashPassword(source.Password)}";
        }
    }
}