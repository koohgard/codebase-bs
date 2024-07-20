using Abstraction.Command.Customer.CustomerOrders;
using MediatR;

namespace Application.Customer;

public class CustomerOrdersQueryHandler : IRequestHandler<CustomerOrdersQuery, IEnumerable<CustomerOrdersQueryResult>>
{
    public CustomerOrdersQueryHandler()
    {
    }

    public async Task<IEnumerable<CustomerOrdersQueryResult>> Handle(CustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        //todo implement 
        throw new NotImplementedException();
    }
}