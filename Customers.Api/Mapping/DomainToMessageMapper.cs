using Customers.Api.Contracts.Messages;
using Customers.Api.Domain;

namespace Customers.Api.Mapping
{
    public static class DomainToMessageMapper
    {
        public static CustomerCreatedMessage ToCustomerCreateMessage(this Customer customer)
        {
            return new CustomerCreatedMessage
            {
                DateOfBirth = customer.DateOfBirth,
                Email = customer.Email,
                FullName = customer.FullName,
                GitHubUsername = customer.GitHubUsername,
                Id = customer.Id,
            };
        }

        public static CustomerUpdatedMessage ToCustomerUpdateMessage(this Customer customer)
        {
            return new CustomerUpdatedMessage
            {
                DateOfBirth = customer.DateOfBirth,
                Email = customer.Email,
                FullName = customer.FullName,
                GitHubUsername = customer.GitHubUsername,
                Id = customer.Id,
            };
        }

        public static CustomerDeletedMessage ToCustomerDeleteMessage(this Customer customer)
        {
            return new CustomerDeletedMessage { Id = customer.Id };
        }
    }
}
