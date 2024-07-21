using Abstraction.Command.Book.CreateBook;
using Abstraction.Enum;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;

namespace Application.BookCommands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookCommandResult>
{
    private readonly AppDbContext appDbContext;
    private readonly IMapper mapper;

    public CreateBookCommandHandler(AppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }
    public async Task<CreateBookCommandResult> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = this.mapper.Map<Domain.Entity.Book>(request);
        book.IsDeleted = false;
        book.OutOfStock = false;
        await appDbContext.AddAsync(book, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        if (request.InitStock > 0)
        {
            var stockTransaction = new StockTransaction()
            {
                BookId = book.Id,
                Count = request.InitStock,
                TransactionFactor = TransactionFactor.Increase,
                TransactionType = TransactionType.AdminUpdate,
                CreateDateTime = DateTime.UtcNow,
            };
            await appDbContext.AddAsync(stockTransaction, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
        return mapper.Map<CreateBookCommandResult>(book);
    }
}