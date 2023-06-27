using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Customers.Api.Messaging
{
    public class SqsMessenger : ISqsMessenger
    {
        private readonly IAmazonSQS _amazonSQS;
        private readonly QueueSettings _queueSettings;
        private string _queueUrl;

        public SqsMessenger(IAmazonSQS amazonSQS, IOptions<QueueSettings> queueSettings)
        {
            _amazonSQS = amazonSQS;
            _queueSettings = queueSettings.Value;
        }

        async Task<SendMessageResponse> ISqsMessenger.SendMessageAsync<T>(T message)
        {

            SendMessageRequest request = new()
            {
                QueueUrl = await GetQueueUrlAsync(),
                MessageBody = JsonSerializer.Serialize(message),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        "MessageType",
                        new MessageAttributeValue
                        {
                            DataType="String", StringValue=typeof(T).Name
                        }
                    }
                }
            };
            SendMessageResponse response = await _amazonSQS.SendMessageAsync(request);
            return response;
        }

        private async Task<string> GetQueueUrlAsync()
        {
            if (_queueUrl is null)
            {
                _queueUrl = (await _amazonSQS.GetQueueUrlAsync(_queueSettings.Name)).QueueUrl;
            }
            return _queueUrl;
        }

   
    }
}
