namespace Cryptocop.Software.API.Services.Publisher
{
    public interface IRabbitMqClient
    {
         void PublishMessage(string routingKey, object payload);
         void Dispose();
    }
}