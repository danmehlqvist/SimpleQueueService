using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

namespace Shared
{
    public class SqsMessageQueue : IMessageQueue
    {
        private readonly AmazonSQSClient _client;
        private readonly string _amazonUrl;
        private const string _messageTypeString = "String";

        public SqsMessageQueue()
        {
            _client = new AmazonSQSClient();
            GetQueueUrlResponse queueUrlResponse = _client.GetQueueUrlAsync("customers").Result;
            _amazonUrl = queueUrlResponse.QueueUrl;
        }

        public async Task<int> SendMessage<T>(T message)
        {
            SendMessageRequest sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _amazonUrl,
                MessageBody = JsonSerializer.Serialize(message),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType= _messageTypeString,
                            StringValue = typeof(T).Name
                        }
                    }
                }
            };

            SendMessageResponse response = await _client.SendMessageAsync(sendMessageRequest);
            return ConvertHtmlStringStatusCodeToInt(response.HttpStatusCode.ToString());
        }

        public async Task PollForMessages<T>(CancellationTokenSource cancellationTokenSource)
        {
            //var cts = new CancellationTokenSource();
            ReceiveMessageRequest recievedMessageRequest = new()
            {
                QueueUrl = _amazonUrl,
                AttributeNames = new List<string> { "All" },
                MessageAttributeNames = new List<string> { "All" },
            };

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                ReceiveMessageResponse response = await _client.ReceiveMessageAsync(recievedMessageRequest, cancellationTokenSource.Token);
                foreach (var message in response.Messages)
                {
                    Console.WriteLine($"Message arrived at {DateTime.Now}");
                    Console.WriteLine($"MessageID: {message.MessageId}");
                    Console.WriteLine($"Body: {message.Body}");
                    await _client.DeleteMessageAsync(_amazonUrl, message.ReceiptHandle, cancellationTokenSource.Token);
                }
                await Task.Delay(1000);
            }
        }

        private int ConvertHtmlStringStatusCodeToInt(string statusCode)
        {
            switch (statusCode)
            {
                case "OK":
                    return 200;
                default:
                    throw new NotImplementedException($"Cannot convert {statusCode} to integer");
            }
        }
    }
}