using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Cryptocop.Software.API.Models;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class ExchangeService : IExchangeService
    {
        private const string CurrenciesUrl = "https://data.messari.io/api/v1/markets?";
        private const string Fields =
            "fields=id,exchange_name,exchange_slug,base_asset_symbol,price_usd,last_trade_at";
        private readonly HttpClient _client = new HttpClient();
        private readonly IMapper _mapper;

        public ExchangeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Envelope<ExchangeDto>> GetExchanges(int pageNumber = 1)
        {
            var pageNumberString = "&page=" + pageNumber;
            var response = await _client.GetAsync(CurrenciesUrl+Fields+pageNumberString);
            response.EnsureSuccessStatusCode();
            var responseDeserialized = await response.DeserializeJsonToList<JObject>(true);
            var exchangeDtoArray = _mapper.Map<IEnumerable<ExchangeDto>>(responseDeserialized);

            using (var objectList = responseDeserialized.GetEnumerator())
            using (var exchangeList = exchangeDtoArray.GetEnumerator())
            {
                while (objectList.MoveNext() && exchangeList.MoveNext())
                {
                    var price = objectList.Current.GetValue("price_usd");
                    if (price != null)
                    {
                        exchangeList.Current.PriceInUsd = (float?) price;
                    }

                    var lastTrade = objectList.Current.GetValue("last_trade_at");
                    if (lastTrade != null)
                    {
                        exchangeList.Current.LastTrade = (DateTime?) lastTrade;
                    }
                }
            }


            return new Envelope<ExchangeDto>(pageNumber, exchangeDtoArray);
            
        }
    }
}