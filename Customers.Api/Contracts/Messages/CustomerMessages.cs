using Customers.Api.Messaging;

namespace Customers.Api.Contracts.Messages
{
    public class CustomerCreatedMessage : IMessage
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string GitHubUsername { get; set; }
    }

    public class CustomerUpdatedMessage : IMessage
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string GitHubUsername { get; set; }
    }

    public class CustomerDeletedMessage : IMessage
    {
        public required Guid Id { get; set; }

    }
}
