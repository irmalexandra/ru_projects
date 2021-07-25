namespace customer_service.Publisher
{
    public interface IRabbitMqClient
    {
         void PublishMessage(string routingKey, object payload);
    }
}