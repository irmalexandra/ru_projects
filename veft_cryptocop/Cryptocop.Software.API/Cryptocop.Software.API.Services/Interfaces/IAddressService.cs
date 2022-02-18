﻿using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Services.Interfaces
{
    public interface IAddressService
    {
        AddressDto AddAddress(string email, AddressInputModel address);
        IEnumerable<AddressDto> GetAllAddresses(string email);
        void DeleteAddress(string email, int addressId);
    }
}