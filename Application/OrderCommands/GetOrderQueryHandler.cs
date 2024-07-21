using Abstraction.Command;
using Abstraction.Command.Order.GetOrder;
using Abstraction.Command.Order.GetOrders;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.OrderCommands;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderQueryResult>
{
    private readonly AppDbContext appDbContext;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetOrderQueryHandler(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetOrderQueryResult> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var query = from OrderDetail in this.appDbContext.OrderDetails
                    where OrderDetail.Id == request.Id
                    select new GetOrderQueryResult
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
        var data = await query.FirstOrDefaultAsync(cancellationToken);
        return data;

    }
}