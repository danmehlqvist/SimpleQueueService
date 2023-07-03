using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System.Text.Json;

namespace Customers.SnsPublisher
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting application");

            var customerCreatedMessage = new CustomerCreatedMessage
            {
                Id= Guid.NewGuid(),
                DateOfBirth = DateTime.Now,
                Email="example@example.com",
                FullName="Dan Banan",
                GitHubUsername = "banan"
            };

            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient();
            Topic topicArnResponse = await snsClient.FindTopicAsync("customers");
            PublishRequest publishRequest = new PublishRequest()
            {
                TopicArn = topicArnResponse.TopicArn,
                Message = JsonSerializer.Serialize(customerCreatedMessage),
                MessageAttributes =
                {
                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType = "String",
                            StringValue = nameof(CustomerCreatedMessage)
                        }
                    }
                }
            };

            PublishResponse response = await snsClient.PublishAsync(publishRequest);
        }
    }
}