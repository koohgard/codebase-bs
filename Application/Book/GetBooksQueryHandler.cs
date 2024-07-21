using Abstraction.Command;
using Abstraction.Command.Book.GetBooks;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Customer;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PagingResult<GetBooksQueryResult>>
{
    private readonly AppDbContext appDbContext;
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetBooksQueryHandler(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<PagingResult<GetBooksQueryResult>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var stockQuery = from st in appDbContext.StockTransactions
                         group st by st.BookId into gItems
                         select new
                         {
                             BookId = gItems.Key,
                             Stock = gItems.Sum(x => x.Count * (int)x.TransactionFactor)
                         };
        
        var query = from book in this.appDbContext.Books
                    join stock in stockQuery on book.Id equals stock.BookId into stocks
                    from stock in stocks.DefaultIfEmpty()
                    select new GetBooksQueryResult
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        Price = book.Price,
                        Stock = stock.Stock,
                        IsDeleted = book.IsDeleted,
                        OutOfStock = book.OutOfStock,
                        RowVersion = book.RowVersion,

                    };

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                              .Take(request.PageSize)
                              .ToListAsync(cancellationToken);
        var totalCount = await query.CountAsync(cancellationToken);
        return new PagingResult<GetBooksQueryResult>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Data = data,
            TotalCount = totalCount
        };
    }
}