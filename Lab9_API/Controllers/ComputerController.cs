using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab9_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Lab9_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private static string exchangeName = "test";
        private static string queueName = "test";
        private static string routingKey = "test";

        private static readonly IConnection conn = GetRabbitConnection();

        [HttpPost]
        public void Post([FromBody] Computer value)
        {
            SendMessage(System.Text.Json.JsonSerializer.Serialize(value));
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
