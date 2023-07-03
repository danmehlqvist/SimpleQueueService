using Amazon.SQS;
using Amazon.SQS.Model;
using Shared;
using Shared.Contracts;

namespace SqsConsumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Starting Consumer");

            string queueName = args.Length > 0 ? args[0] : "customers";
            await Console.Out.WriteLineAsync($"Queue name: {queueName}");

            IMessageQueue messageQueue = new SqsMessageQueue(queueName);
            await messageQueue.PollForMessages<CustomerCreated>(new CancellationTokenSource());
        }
    }
}