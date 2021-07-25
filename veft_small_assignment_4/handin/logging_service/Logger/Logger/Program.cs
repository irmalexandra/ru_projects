using System;
using System.Runtime;
using System.Security.Authentication;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Logger
{

    class Logger
    {
        
        public static void Main()
        {
            
            
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "logging_queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                channel.QueueBind("logging_queue", "order_exchange", "order_created");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = "Log: " + Encoding.UTF8.GetString(body);
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"log.txt", true))
                    {
                        file.WriteLine(message);
                    }

                    Console.WriteLine(" [x] Received:\n{0}", message);
                };
                channel.BasicConsume(queue: "logging_queue",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}