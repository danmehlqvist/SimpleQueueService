using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Customers.Api.Messaging
{
    public class SnsMessenger : ISnsMessenger
    {
        private readonly IAmazonSimpleNotificationService _amazonSNS;
        private readonly QueueSettings _queueSettings;
        private string _topicArn;

        public SnsMessenger(IAmazonSimpleNotificationService amazonSNS, IOptions<QueueSettings> queueSettings)
        {
            _amazonSNS = amazonSNS;
            _queueSettings = queueSettings.Value;
        }

        async Task<PublishResponse> ISnsMessenger.SendMessageAsync<T>(T message)
        {

            PublishRequest request = new()
            {
                TopicArn = await this.GetTopicArn(),
                Message = JsonSerializer.Serialize(message),
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
            PublishResponse response = await _amazonSNS.PublishAsync(request);
            return response;
        }

        private async Task<string> GetTopicArn()
        {
            if (_topicArn is null)
            {
                _topicArn = (await _amazonSNS.FindTopicAsync("customers")).TopicArn;
            }
            return _topicArn;
        }

   
    }
}
