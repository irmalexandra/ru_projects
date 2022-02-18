using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Repositories.Interfaces;
using TechnicalRadiation.Models.Exceptions;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddressRepository(CryptocopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AddressDto AddAddress(string email, AddressInputModel address)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }

            var addressEntity = _mapper.Map<Address>(address);
            addressEntity.UserId = currentUser.Id;
            _dbContext.Addresses.Add(addressEntity);
            _dbContext.SaveChanges();

            return _mapper.Map<AddressDto>(addressEntity);

        }

        public IEnumerable<AddressDto> GetAllAddresses(string email)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }
            
            var addresses = _dbContext.Addresses
                                        .Where(address => address.User.Email == email)
                                        .Select(address => _mapper.Map<AddressDto>(address)).ToList();
            return addresses;
            
        }

        public void DeleteAddress(string email, int addressId)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }

            _dbContext.Addresses.Remove(
                _dbContext.Addresses.FirstOrDefault(address => address.Id == addressId && address.User.Email == email)!);
            _dbContext.SaveChanges();
        }
    }
}