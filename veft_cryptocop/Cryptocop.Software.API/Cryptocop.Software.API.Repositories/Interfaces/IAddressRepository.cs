﻿using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        AddressDto AddAddress(string email, AddressInputModel address);
        IEnumerable<AddressDto> GetAllAddresses(string email);
        void DeleteAddress(string email, int addressId);
    }
}