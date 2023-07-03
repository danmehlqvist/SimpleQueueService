using Amazon.SQS;
using Amazon.SQS.Model;
using MediatR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Customers.Consumer
{
    public class QueueConsumerService : BackgroundService
    {
        private readonly IAmazonSQS _amazonSQS;
        private readonly IMediator _mediator;
        private readonly ILogger<QueueConsumerService> _logger;
        private readonly QueueSettings _queueSettings;

        public QueueConsumerService(IAmazonSQS amazonSQS, IOptions<QueueSettings> options, IMediator mediator, ILogger<QueueConsumerService> logger)
        {
            _amazonSQS = amazonSQS;
            _mediator = mediator;
            _logger = logger;
            _queueSettings = options.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueUrlResponse = await _amazonSQS.GetQueueUrlAsync(_queueSettings.Name, stoppingToken);
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

                    var type = Type.GetType($"Customers.Consumer.{messageType}");
                    if (type is null)
                    {
                        _logger.LogError("Could not load type {MessageType} for passing into Mediatr", messageType);
                        continue;
                    }

                    try { 
                        IMessage typedMessage = (IMessage)JsonSerializer.Deserialize(message.Body, type)!;
                        await _mediator.Send(typedMessage);
                    }catch (Exception ex)
                    {
                        _logger.LogError("Exception of type {ExceptionType} with message {ExceptionMessage}", ex.GetType().Name, ex.Message);
                        continue;
                    }

                    await _amazonSQS.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle, stoppingToken);
                }

                await Console.Out.WriteLineAsync($"{DateTime.UtcNow.ToLongTimeString()} .");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
