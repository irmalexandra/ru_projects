using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Cryptocop.Software.API.Services.Publisher
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;

        public RabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;
            try
            {
                var messageBrokerSection = configuration.GetSection("MessageBroker");
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(messageBrokerSection.GetSection("ConnectionString").Value)
                };
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
                _channel.ExchangeDeclare(messageBrokerSection.GetSection("ExchangeName").Value, "direct", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PublishMessage(string routingKey, object payload)
        {
            var messageBrokerSection = _configuration.GetSection("MessageBroker");
            _channel.BasicPublish(
                exchange: messageBrokerSection.GetSection("ExchangeName").Value,
                routingKey,
                mandatory: true,
                basicProperties: null,
                body: ConvertJsonToBytes(payload));
        }

        public void Dispose()
        {
            _channel.Close();
            _channel.Dispose();
        }

        private byte[] ConvertJsonToBytes(object obj) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
    }
}
