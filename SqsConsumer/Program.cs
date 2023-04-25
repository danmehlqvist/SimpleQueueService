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

            IMessageQueue messageQueue = new SqsMessageQueue();
            await messageQueue.PollForMessages<CustomerCreated>(new CancellationTokenSource());
        }
    }
}