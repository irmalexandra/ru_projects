using System;
using System.Linq;
using System.Text;
using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Repositories.Helpers;
using Cryptocop.Software.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TechnicalRadiation.Models.Exceptions;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    
    public class UserRepository : IUserRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;
        private readonly string _salt = "e3ba0c68-b9d7-4a29-809c-14c6d73a46c9";

        public UserRepository(CryptocopDbContext dbContext, ITokenRepository tokenRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
        }

        public UserDto CreateUser(RegisterInputModel inputModel)
        {

            var duplicate = _dbContext.Users.FirstOrDefault(user => user.Email == inputModel.Email);

            if (duplicate != null){ throw new ResourceAlreadyExistsException(); }
            
            var userEntity = _mapper.Map<User>(inputModel);
            _dbContext.Users.Add(userEntity);
            _dbContext.SaveChanges();

            var token = _tokenRepository.CreateNewToken();
            var userDto = _mapper.Map<UserDto>(userEntity);

            userDto.TokenId = token.Id;
            return userDto;
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == loginInputModel.Email &&
                                                            u.HashedPassword == HashingHelper.HashPassword(loginInputModel.Password));
            if (user == null) { return null; }

            var token = new JwtToken();
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();
            
            var userDto = _mapper.Map<UserDto>(user);
            userDto.TokenId = token.Id;
            
            return userDto;
        }


    }
}