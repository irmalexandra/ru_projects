using System.Collections.Generic;
using Newtonsoft.Json;

namespace api_gateway.Models.InputModels
{
    public class OrderInputModel
    {
        [JsonProperty(PropertyName = "customerId")]
        public int CustomerId { get; set; }
        [JsonProperty(PropertyName = "paymentType")]
        public string PaymentType { get; set; }
        [JsonProperty(PropertyName = "items")]
        public IEnumerable<OrderItemInputModel> Items { get; set; }
    }
}
