using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

namespace SqsPublisher
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://sqs.eu-north-1.amazonaws.com/742873788565/customers
            AmazonSQSClient client = new AmazonSQSClient();

            CustomerCreated customer = new CustomerCreated(
                "danbanan@example.com",
                "Dan Banan",
                new DateTime(1976, 01, 07),
                "danbanan"
                );

            GetQueueUrlResponse amazonUrl = await client.GetQueueUrlAsync("customers");

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = amazonUrl.QueueUrl,
                MessageBody = JsonSerializer.Serialize(customer),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType="string",
                            StringValue = nameof(CustomerCreated)
                        }
                    }
                }
            };

            SendMessageResponse response = await client.SendMessageAsync(sendMessageRequest);

            Console.WriteLine($"{response.HttpStatusCode}");
            Console.ReadKey();
        }
    }
}