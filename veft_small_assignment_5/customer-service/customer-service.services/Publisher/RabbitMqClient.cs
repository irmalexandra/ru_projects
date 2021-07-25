using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace customer_service.Publisher
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private IModel _channel;
        private IConfiguration Configuration;

        public RabbitMqClient(IConfiguration configuration)
        {
            Configuration = configuration;
            try
            {
                var messageBrokerSection = configuration.GetSection("MessageBroker");
                var factory = new ConnectionFactory();
                factory.Uri = new Uri(messageBrokerSection.GetSection("ConnectionString").Value);
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PublishMessage(string routingKey, object payload)
        {
            var messageBrokerSection = Configuration.GetSection("MessageBroker");
            _channel.BasicPublish(
                exchange: messageBrokerSection.GetSection("ExchangeName").Value,
                routingKey,
                mandatory: true,
                basicProperties: null,
                body: ConvertJsonToBytes(payload));
        }

        private byte[] ConvertJsonToBytes(object obj) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
    }
}
