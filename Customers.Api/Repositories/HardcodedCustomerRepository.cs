using Customers.Api.Contracts.Data;
using Customers.Api.Domain;
using Customers.Api.Mapping;

namespace Customers.Api.Repositories
{
    public class HardcodedCustomerRepository : ICustomerRepository
    {
        private List<Customer> _customers;

        public HardcodedCustomerRepository()
        {
            _customers = new List<Customer>
            {
                new Customer
                {
                    DateOfBirth = DateTime.Now,
                    Email="email",
                    FullName = "fullName",
                    GitHubUsername = "gitHubUsername",
                    Id= Guid.NewGuid()
                }
            };
        }

        public Task<bool> CreateAsync(CustomerDto customerDto)
        {
            Customer customer = customerDto.ToCustomer();
            _customers.Add(customer);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            bool success;

            Customer customerToDelete = _customers.SingleOrDefault(x => x.Id == id);
            if (customerToDelete is null)
            {
                success = false;
            }
            else
            {
                _customers.Remove(customerToDelete);
                success = true;
            }
            return Task.FromResult(success);
        }

        public Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            return Task.FromResult(_customers.ToCustomerDtos());
        }

        public Task<CustomerDto> GetAsync(Guid id)
        {
            Customer customer = _customers.SingleOrDefault(x => x.Id == id);
            if (customer is null) return Task.FromResult(null as CustomerDto);

            return Task.FromResult(customer.ToCustomerDto());
        }

        public Task<bool> UpdateAsync(CustomerDto customerDto)
        {
            Customer customerToBeUpdated = _customers.SingleOrDefault(x => x.Id == customerDto.Id);
            if (customerToBeUpdated is null)
            {
                return Task.FromResult(false);
            }

            throw new NotImplementedException();
        }
    }
}
