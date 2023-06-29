using MediatR;

namespace Customers.Consumer.Handlers
{
    public class CustomerDeletedHandler : IRequestHandler<CustomerDeletedMessage>
    {
        public Task Handle(CustomerDeletedMessage request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
