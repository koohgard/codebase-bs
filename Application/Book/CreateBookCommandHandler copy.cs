using Abstraction.Command.Book.UpdateStock;
using Abstraction.Enum;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;

namespace Application.Customer;

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

        // return mapper.Map<UpdateStockCommandResult>();
        return new UpdateStockCommandResult();
    }
}