using MediatR;

namespace Customers.Consumer.Handlers
{
    public class CustomerCreatedHandler : IRequestHandler<CustomerCreatedMessage>
    {
        private readonly ILogger<CustomerCreatedHandler> _logger;

        public CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(CustomerCreatedMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Message of type CustomerCreatedMessage recieved: Full name: {request.FullName}");
            return Task.CompletedTask;
        }
    }
}
