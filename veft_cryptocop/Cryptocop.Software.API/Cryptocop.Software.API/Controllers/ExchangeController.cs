using System.Threading.Tasks;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/exchanges")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {

        private readonly IExchangeService _exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [Route("{pageNumber:int}", Name = "GetAllExchanges")]
        [HttpGet]
        public async Task<IActionResult> GetAllExchanges(int pageNumber)
        {
            var allExchanges = await _exchangeService.GetExchanges(pageNumber);
            return Ok(allExchanges);
        }
    }
    
}