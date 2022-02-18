using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptocop.Software.API.Models.Dtos;

namespace Cryptocop.Software.API.Services.Interfaces
{
    public interface ICryptoCurrencyService
    {
        Task<IEnumerable<CryptocurrencyDto>> GetAvailableCryptocurrencies();
    }


}