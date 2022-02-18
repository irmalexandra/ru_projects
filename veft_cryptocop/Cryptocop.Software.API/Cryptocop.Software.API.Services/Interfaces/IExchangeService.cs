using System.Threading.Tasks;
using Cryptocop.Software.API.Models;
using Cryptocop.Software.API.Models.Dtos;

namespace Cryptocop.Software.API.Services.Interfaces
{
    public interface IExchangeService
    {
        Task<Envelope<ExchangeDto>> GetExchanges(int pageNumber = 1);
    }
}