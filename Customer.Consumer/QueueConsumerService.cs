using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Customers.Consumer
{
    public class QueueConsumerService : BackgroundService
    {
        private readonly IAmazonSQS _amazonSQS;
        private readonly QueueSettings _queueSettings;

        public QueueConsumerService(IAmazonSQS amazonSQS, IOptions<QueueSettings> options)
        {
            _amazonSQS = amazonSQS;
            _queueSettings = options.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueUrlResponse = await _amazonSQS.GetQueueUrlAsync("customers", stoppingToken);
            var recievedMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                AttributeNames = new List<string> { "All" },
                MessageAttributeNames = new List<string> { "All" },
            };

            await Console.Out.WriteLineAsync("Starting polling for messages");

            while (!stoppingToken.IsCancellationRequested)
            {
                ReceiveMessageResponse response = await _amazonSQS.ReceiveMessageAsync(recievedMessageRequest, stoppingToken);

                foreach (var message in response.Messages)
                {
                    string messageType = message.MessageAttributes["MessageType"].StringValue;

                    switch (messageType)
                    {
                        case nameof(CustomerCreatedMessage):
                            CustomerCreatedMessage customerCreatedMessage = JsonSerializer.Deserialize<CustomerCreatedMessage>(message.Body) ?? throw new JsonException("Could not deserialize message");
                            Console.WriteLine($"Created: {customerCreatedMessage.FullName}");
                            break;
                        case nameof(CustomerUpdatedMessage):
                            break;
                        case nameof(CustomerDeletedMessage):
                            break;
                        default:
                            throw new NotImplementedException($"Parsing of message of type {messageType} not implemented");
                    }

                    await _amazonSQS.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle, stoppingToken);
                }

                await Console.Out.WriteLineAsync(".");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
