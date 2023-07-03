using Amazon.SimpleNotificationService.Model;

namespace Customers.Api.Messaging
{
    public interface ISnsMessenger
    {
        Task<PublishResponse> SendMessageAsync<T>(T message) where T : IMessage;
    }
}
