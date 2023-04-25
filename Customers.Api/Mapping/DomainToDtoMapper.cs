using Customers.Api.Contracts.Data;
using Customers.Api.Domain;

namespace Customers.Api.Mapping;

public static class DomainToDtoMapper
{
    public static CustomerDto? ToCustomerDto(this Customer customer)
    {
        if (customer is null) return null;
        return new CustomerDto
        {
            Id = customer.Id,
            Email = customer.Email,
            GitHubUsername = customer.GitHubUsername,
            FullName = customer.FullName,
            DateOfBirth = customer.DateOfBirth
        };
    }

    public static IEnumerable<CustomerDto> ToCustomerDtos(this List<Customer> customers)
    {
        return customers.Select(x => x.ToCustomerDto()!);
    }
}
