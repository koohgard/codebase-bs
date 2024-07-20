using Abstraction.Command;
using Abstraction.Command.Customer.GetCustomerOrders;
using MediatR;

namespace Application.Customer;

public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, PagingResult<GetCustomerOrdersQueryResult>>
{
    public GetCustomerOrdersQueryHandler()
    {
    }

    public async Task<PagingResult<GetCustomerOrdersQueryResult>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        //TODO implement 
        throw new NotImplementedException();
    }
}