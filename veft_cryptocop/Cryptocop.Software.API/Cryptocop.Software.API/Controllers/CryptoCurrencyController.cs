using System.Threading.Tasks;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/cryptocurrencies")]
    [ApiController]
    public class CryptoCurrencyController : ControllerBase
    {

        private readonly ICryptoCurrencyService _cryptoCurrencyService;

        public CryptoCurrencyController(ICryptoCurrencyService cryptoCurrencyService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
        }

        [Route("", Name = "GetAllCryptocurrencies")]
        [HttpGet]
        public async Task<IActionResult> GetAllCryptocurrencies()
        {
            var allCurrencies = await _cryptoCurrencyService.GetAvailableCryptocurrencies();
            return Ok(allCurrencies);
        }
        
    }
}
