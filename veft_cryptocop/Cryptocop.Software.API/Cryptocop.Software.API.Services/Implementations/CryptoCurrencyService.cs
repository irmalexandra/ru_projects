using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private const string CurrenciesUrl = "https://data.messari.io/api/v2/assets?";
        private const string Fields =
            "fields=id,symbol,name,slug,metrics/market_data/price_usd,profile/general/overview/project_details";
        private static readonly List<string> AvailableCurrencies = new List<string>(){"BTC", "ETH", "USDT", "XMR", 
            "bitcoin", "ethereum", "tether", "monero", "Bitcoin", "Ethereum", "Tether", "Monero"};
        private readonly HttpClient _client = new HttpClient();
        private readonly IMapper _mapper;

        public CryptoCurrencyService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<CryptocurrencyDto>> GetAvailableCryptocurrencies()
        {
            var response = await _client.GetAsync(CurrenciesUrl+Fields);
            response.EnsureSuccessStatusCode();
            var responseDeserialized = await response.DeserializeJsonToList<JObject>(true);
            var currencyDtoArray = _mapper.Map<IEnumerable<CryptocurrencyDto>>(responseDeserialized);
                
            return currencyDtoArray.Where(curr => AvailableCurrencies.Any(l => curr.Symbol == l));
        }
    }
}