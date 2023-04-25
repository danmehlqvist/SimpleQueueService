using Customers.Api.Contracts.Data;
using Customers.Api.Domain;

namespace Customers.Api.Mapping;

public static class DtoToDomainMapper
{
    public static Customer ToCustomer(this CustomerDto customerDto)
    {
        return new Customer
        {
            Id = customerDto.Id,
            Email = customerDto.Email,
            GitHubUsername = customerDto.GitHubUsername,
            FullName = customerDto.FullName,
            DateOfBirth = customerDto.DateOfBirth
        };
    }

    public static IEnumerable<Customer> ToCustomers(this List<CustomerDto> customers)
    {
        return customers.Select(x => x.ToCustomer());
    }
}
