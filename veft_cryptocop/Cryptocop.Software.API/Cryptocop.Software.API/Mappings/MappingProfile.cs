using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Helpers;
using Newtonsoft.Json.Linq;

namespace Cryptocop.Software.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterInputModel, User>()
                .ForMember(user => user.HashedPassword, options => options.MapFrom<HashPasswordResolver>());
            CreateMap<User, UserDto>();
            CreateMap<AddressInputModel, Address>();
            CreateMap<Address, AddressDto>();
            CreateMap<Address, Order>();
            CreateMap<PaymentCard, Order>()
                .ForMember(order => order.MaskedCreditCard,
                    options => options.MapFrom(source =>
                        source.CardNumber.MaskPaymentCard(0, source.CardNumber.Length - 4, '*')))
                .ForMember(order => order.Id, opt => opt.Ignore());

            CreateMap<User, Order>();
            CreateMap<Order, OrderDto>()
                .ForMember(order => order.OrderDate,
                    options => options.MapFrom(source => source.OrderDate.ToString("dd.MM.yyyy")))
                .ForMember(order => order.CreditCard, options => options.MapFrom(source => source.MaskedCreditCard));
            CreateMap<PaymentCardInputModel, PaymentCard>();
            CreateMap<PaymentCard, PaymentCardDto>();
            CreateMap<ShoppingCartItemInputModel, ShoppingCartItem>();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
                .ForMember(item => item.TotalPrice, options => options.MapFrom(source => source.Quantity*source.UnitPrice));
            CreateMap<ShoppingCartItemDto, OrderItem>()
                .ForMember(item => item.Id, opt => opt.Ignore());
            CreateMap<OrderItem, OrderItemDto>();
            
            CreateMap<JObject, CryptocurrencyDto>()
                .ForMember(currency => currency.Id, options => options.MapFrom(source => source.GetValue("id")))
                .ForMember(currency => currency.Name, options => options.MapFrom(source => source.GetValue("name")))
                .ForMember(currency => currency.Slug, options => options.MapFrom(source => source.GetValue("slug")))
                .ForMember(currency => currency.Symbol, options => options.MapFrom(source => source.GetValue("symbol")))
                .ForMember(currency => currency.PriceInUsd, options => options.MapFrom(source => source.GetValue("price_usd")))
                .ForMember(currency => currency.ProjectDetails, options => options.MapFrom(source => source.GetValue("project_details")));

            CreateMap<JObject, ExchangeDto>()
                .ForMember(exchange => exchange.Id, options => options.MapFrom(source => source.GetValue("id")))
                .ForMember(exchange => exchange.Name,
                    options => options.MapFrom(source => source.GetValue("exchange_name")))
                .ForMember(exchange => exchange.Slug,
                    options => options.MapFrom(source => source.GetValue("exchange_slug")))
                .ForMember(exchange => exchange.AssetSymbol,
                    options => options.MapFrom(source => source.GetValue("base_asset_symbol")));
        }
    }
}