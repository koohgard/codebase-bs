using Abstraction.Command;
using Abstraction.Command.Order.GetOrders;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.OrderCommands;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PagingResult<GetOrdersQueryResult>>
{
    private readonly AppDbContext appDbContext;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetOrdersQueryHandler(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<PagingResult<GetOrdersQueryResult>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = from OrderDetail in this.appDbContext.OrderDetails
                    where OrderDetail.Order.CreatedDateTime < request.EndDate &&
                          OrderDetail.Order.CreatedDateTime > request.StartDate
                    select new GetOrdersQueryResult
                    {
                        Id = OrderDetail.Id,
                        BookId = OrderDetail.BookId,
                        BookTitle = OrderDetail.Book.Title,
                        BookPrice = OrderDetail.Book.Price,
                        Count = OrderDetail.Count,
                        TotalPrice = OrderDetail.Book.Price * OrderDetail.Count,
                        OrderId = OrderDetail.Order.Id,
                        UserId = OrderDetail.Order.UserId,
                        UserEmail = OrderDetail.Order.User.Email,
                        CreatedDateTime = OrderDetail.Order.CreatedDateTime,
                        OrderStatus = OrderDetail.Order.OrderStatus,
                    };
        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                              .Take(request.PageSize)
                              .ToListAsync(cancellationToken);
        var totalCount = await query.CountAsync(cancellationToken);
        return new PagingResult<GetOrdersQueryResult>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Data = data,
            TotalCount = totalCount
        };
    }
}