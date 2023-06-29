using MediatR;

namespace Customers.Consumer.Handlers
{
    public class CustomerUpdatedHandler : IRequestHandler<CustomerUpdatedMessage>
    {
        public Task Handle(CustomerUpdatedMessage request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
