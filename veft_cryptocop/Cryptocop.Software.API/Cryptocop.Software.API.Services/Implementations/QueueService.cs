using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Services.Publisher;
using Microsoft.VisualBasic;


namespace Cryptocop.Software.API.Services.Implementations
{
    public class QueueService : IQueueService, IDisposable
    {
        
        private readonly IRabbitMqClient _mbClient;


        public QueueService(IRabbitMqClient mbClient)
        {
            _mbClient = mbClient;
        }
        
        public void PublishMessage(string routingKey, object body)
        {
            var jsonBody = JsonConvert.SerializeObject(body);
            _mbClient.PublishMessage(routingKey, jsonBody);
        }

        public void Dispose()
        {
            _mbClient.Dispose();
        }
    }
    
}