using Cryptocop.Software.API.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Services.Helpers;
using Newtonsoft.Json.Linq;

namespace Cryptocop.Software.API.Services.Implementations
{
    
    public class ShoppingCartService : IShoppingCartService
    {
        private const string CurrencyUrl = "https://data.messari.io/api/v1/assets/";
        private const string MessariApiKey = "88cc1425-f23d-4e8f-abaf-29329fbc5c89";
        private const string Fields =
            "/metrics?fields=market_data/price_usd";
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly HttpClient _client = new HttpClient();
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ICryptoCurrencyService cryptoCurrencyService, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            return _shoppingCartRepository.GetCartItems(email);
        }

        public async Task<ShoppingCartItemDto> AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItem)
        {
            var response = await _client.GetAsync(CurrencyUrl+shoppingCartItem.ProductIdentifier+Fields);
            response.EnsureSuccessStatusCode();
            var responseDeserialized = await response.DeserializeJsonToObject<JObject>(true);
            return _shoppingCartRepository.AddCartItem(email, shoppingCartItem, responseDeserialized["price_usd"].ToObject<float>());
        }

        public void RemoveCartItem(string email, int id)
        {
            _shoppingCartRepository.RemoveCartItem(email, id);
        }

        public void UpdateCartItemQuantity(string email, int id, float quantity)
        {
            _shoppingCartRepository.UpdateCartItemQuantity(email, id, quantity);
        }

        public void ClearCart(string email)
        {
            _shoppingCartRepository.ClearCart(email);
        }
    }
}
