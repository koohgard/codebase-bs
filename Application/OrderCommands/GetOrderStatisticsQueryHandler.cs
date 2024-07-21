using Abstraction.Command.Order.GetStatistics;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.OrderCommands;

public class GetOrderStatisticsQueryHandler : IRequestHandler<GetOrderStatisticsQuery, IEnumerable<GetOrderStatisticsQueryResult>>
{
    private readonly AppDbContext appDbContext;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetOrderStatisticsQueryHandler(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<IEnumerable<GetOrderStatisticsQueryResult>> Handle(GetOrderStatisticsQuery request, CancellationToken cancellationToken)
    {
        var query = from OrderDetail in this.appDbContext.OrderDetails
                    select new
                    {
                        OrderDetail.Count,
                        TotalPrice = OrderDetail.Book.Price * OrderDetail.Count,
                        OrderDetail.Order.CreatedDateTime.Month,
                        OrderDetail.Order.CreatedDateTime.Year
                    };

        var groupQuery = from item in query
                         group item by new { item.Year, item.Month } into gItems
                         select new GetOrderStatisticsQueryResult
                         {
                             Month = gItems.Key.Month,
                             Year = gItems.Key.Year,
                             TotalOrderCount = gItems.Count(),
                             TotalBookCount = gItems.Sum(x => x.Count),
                             TotalPurchaseAmount = gItems.Sum(x => x.TotalPrice)
                         };
        var result = groupQuery.ToList();
        return result;
    }
}