using Abstraction.Command;
using Abstraction.Command.Customer.GetCustomerOrders;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Customer;

public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, PagingResult<GetCustomerOrdersQueryResult>>
{
    private readonly AppDbContext appDbContext;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetCustomerOrdersQueryHandler(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.httpContextAccessor = httpContextAccessor;
    }


    public async Task<PagingResult<GetCustomerOrdersQueryResult>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        var userId = Utils.GetCurrentUserId(httpContextAccessor.HttpContext);
        var query = from OrderDetail in this.appDbContext.OrderDetails
                    select new GetCustomerOrdersQueryResult
                    {
                        Id = OrderDetail.Id,
                        BookId = OrderDetail.BookId,
                        BookTitle = OrderDetail.Book.Title,
                        BookPrice = OrderDetail.Book.Price,
                        Count = OrderDetail.Count,
                        TotalPrice = OrderDetail.Book.Price * OrderDetail.Count,
                        OrderId = OrderDetail.Order.Id,
                        UserId = OrderDetail.Order.UserId,
                        CreatedDateTime = OrderDetail.Order.CreatedDateTime,
                        OrderStatus = OrderDetail.Order.OrderStatus,
                    };
        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                              .Take(request.PageSize)
                              .ToListAsync(cancellationToken);
        var totalCount = await query.CountAsync(cancellationToken);
        return new PagingResult<GetCustomerOrdersQueryResult>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Data = data,
            TotalCount = totalCount
        };
    }
}