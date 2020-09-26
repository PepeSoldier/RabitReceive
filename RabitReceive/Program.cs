using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

class Receive
{
    private static List<string> queData;
    public static void Main()
    {
        queData = new List<string>();
        string data = "";
        var factory = new ConnectionFactory() { HostName = "10.10.1.4", UserName = "oneprod", Password = "c5mvmZN2kqOrVKVYFRs_XX2sVfZBVUun" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "test-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                data += message;
                queData.Add(message.ToString());
                Console.WriteLine(" Press [enter] to exit." + message);
                System.Diagnostics.Debug.WriteLine(message.ToString());
            };
            channel.BasicConsume(queue: "test-queue", autoAck: true, consumer: consumer);
            Console.WriteLine("2Pack changes");
            Console.WriteLine("2Pack 2changes");
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        
        }
       // Console.WriteLine(" Press [ESC] to exit.");
        
    }
}