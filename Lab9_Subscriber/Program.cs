using RabbitMQ.Client;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace Lab9_Subscriber
{
    class Program
    {
        private static string exchangeName = "test";
        private static string queueName = "test";
        private static string routingKey = "test";

        private static readonly IConnection conn = GetRabbitConnection();
        private static readonly SqlConnection sqlConn = new SqlConnection(@"Server=DESKTOP-LANBNAJ\SQLEXPRESS;Database=Rabbit;Integrated Security=False;Persist Security Info=True;Trusted_Connection=True;MultipleActiveResultSets=True;");

        static void Main(string[] args)
        {
            Console.WriteLine(" --- Message Consumer ---");
            var thread = new Thread(new ThreadStart(RabbitListener));
            thread.Start();
            thread.Join();

            conn.Close();
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
            return factory.CreateConnection();
        }

        private static IModel GetRabbitChannel(string exchangeName, string queueName, string routingKey)
        {
            IModel model = conn.CreateModel();
            model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            model.QueueDeclare(queueName, false, false, false, null);
            model.QueueBind(queueName, exchangeName, routingKey, null);
            return model;
        }

        private static void RabbitListener()
        {
            IModel model = GetRabbitChannel(exchangeName, queueName, routingKey);
            var subscription = new RabbitMQ.Client.MessagePatterns.Subscription(model, queueName, false);
            while (true)
            {
                var basicDeliveryEventArgs = subscription.Next();
                string messageContent = Encoding.UTF8.GetString(basicDeliveryEventArgs.Body);

                // action
                try
                {
                    var computer = System.Text.Json.JsonSerializer.Deserialize<Computer>(messageContent);
                    HandleMessage(computer);
                }
                catch(Exception e)
                {
                    HandleMessage(messageContent);
                }

                subscription.Ack(basicDeliveryEventArgs);
            }
        }

        private static void HandleMessage(Computer computer)
        {
            Console.WriteLine("Received computer");

            try
            {
                var command = new SqlCommand(@"INSERT INTO [dbo].[Computers]([Name], [IsDiscreteVideoCard], [CPU], [RAM]) 
                    VALUES (@Name, @IsDiscreteVideoCard, @CPU, @RAM)", sqlConn);
                command.Parameters.AddWithValue("CPU", computer.CPU);
                command.Parameters.AddWithValue("IsDiscreteVideoCard", computer.IsDiscreteVideoCard);
                command.Parameters.AddWithValue("Name", computer.Name);
                command.Parameters.AddWithValue("RAM", computer.RAM);
                sqlConn.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                sqlConn.Close();
            }
        }

        private static void HandleMessage(string message)
        {
            Console.WriteLine("Received message: " + message);

            try
            {
                var command = new SqlCommand(@"INSERT INTO [dbo].[Messages]([Message]) VALUES (@message)", sqlConn);
                command.Parameters.AddWithValue("message", message);
                sqlConn.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                sqlConn.Close();
            }
        }










        private string ReceiveIndividualMessage()
        {
            string originalMessage = "";
            IModel model = GetRabbitChannel(exchangeName, queueName, routingKey);
            BasicGetResult result = model.BasicGet(queueName, false);
            if (result == null)
            {
                // В настоящее время нет доступных сообщений.
            }
            else
            {
                byte[] body = result.Body;
                originalMessage = Encoding.UTF8.GetString(body);
            }
            return originalMessage;
        }
    }
}
