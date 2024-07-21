using Abstraction.Command.Book.UpdateStock;
using Abstraction.Enum;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BookCommands;

public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, UpdateStockCommandResult>
{
    private readonly AppDbContext appDbContext;
    private readonly IMapper mapper;

    public UpdateStockCommandHandler(AppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }
    public async Task<UpdateStockCommandResult> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var stockTransaction = new StockTransaction()
        {
            BookId = request.BookId,
            Count = request.Count,
            TransactionFactor = request.TransactionFactor,
            CreateDateTime = DateTime.UtcNow,
        };
        await appDbContext.AddAsync(stockTransaction, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);

        var book = await this.appDbContext.Books.FirstOrDefaultAsync(x => x.Id == request.BookId, cancellationToken);
        var stock = await appDbContext.StockTransactions.Where(x => x.BookId == request.BookId)
                                                        .SumAsync(x => x.Count * (int)x.TransactionFactor, cancellationToken);
        book.OutOfStock = stock <= 0;
        appDbContext.Update<Book>(book);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return new UpdateStockCommandResult()
        {
            BookId = book.Id,
            Stock = stock,
            BookTitle = book.Title
        };
    }
}