using Amazon.SQS;
using Amazon.SQS.Model;
using Shared;
using Shared.Contracts;
using System.Text.Json;

namespace SqsPublisher
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Starting publisher");

            IMessageQueue messageQueue = new SqsMessageQueue();

            Console.WriteLine($"Press [any key] to send message");

            int messageCount = 1;

            while (true)
            {
                Console.ReadKey();

                CustomerCreated customer = new CustomerCreated(
                    $"Message {messageCount++}",
                    "Dan Banan",
                    new DateTime(1976, 01, 07),
                    "nytt meddelande"
                    );

                int statusCode = await messageQueue.SendMessage<CustomerCreated>(customer);

                Console.WriteLine($"Message sent with status code {statusCode}");
            }


        }
    }
}