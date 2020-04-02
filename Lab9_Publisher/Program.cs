using RabbitMQ.Client;
using System;

namespace Lab9_Publisher
{
    class Program
    {
        private static string exchangeName = "test";
        private static string queueName = "test";
        private static string routingKey = "test";

        private static readonly IConnection conn = GetRabbitConnection();

        static void Main(string[] args)
        {
            Console.WriteLine(" --- Message Producer --- (write message + Enter)");

            var message = string.Empty;
            while (message.ToLowerInvariant() != "x")
            {
                message = Console.ReadLine();
                SendMessage(message);
            }
        }

        private static IConnection GetRabbitConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "localhost"
            };
            IConnection conn = factory.CreateConnection();
            return conn;
        }

        private static IModel GetRabbitChannel(string exchangeName, string queueName, string routingKey)
        {
            IModel model = conn.CreateModel();
            model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            model.QueueDeclare(queueName, false, false, false, null);
            model.QueueBind(queueName, exchangeName, routingKey, null);
            return model;
        }

        private static void SendMessage(string message)
        {
            IModel model = GetRabbitChannel(exchangeName, queueName, routingKey);
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);
            model.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
        }
    }
}
