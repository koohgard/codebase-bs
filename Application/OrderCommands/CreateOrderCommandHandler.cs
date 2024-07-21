using Abstraction.Command.Order.CreateOrder;
using Abstraction.Enum;
using Abstraction.Exception;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.OrderCommands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResult>
{
    private readonly AppDbContext appDbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public CreateOrderCommandHandler(AppDbContext appDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var book = await this.appDbContext.Books.FirstOrDefaultAsync(x => x.Id == request.BookId, cancellationToken);
        var stock = await appDbContext.StockTransactions.Where(x => x.BookId == request.BookId)
                                                        .SumAsync(x => x.Count * (int)x.TransactionFactor, cancellationToken);
        if (stock < request.Count)
        {
            throw new OutOfStockException();
        }
        var currentUserId = Utils.GetCurrentUserId(this.httpContextAccessor.HttpContext);
        var order = new Order();
        order.CreatedDateTime = DateTime.UtcNow;
        order.OrderStatus = OrderStatus.Order;
        order.UserId = currentUserId;
        await appDbContext.AddAsync(order, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);

        var stockTransaction = new StockTransaction()
        {
            BookId = request.BookId,
            Count = request.Count,
            TransactionFactor = TransactionFactor.Decrease,
            TransactionType = TransactionType.AdminUpdate,
            CreateDateTime = DateTime.UtcNow,
        };
        await appDbContext.AddAsync(stockTransaction, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);

        var OrderDetail = new OrderDetail()
        {
            OrderId = order.Id,
            BookId = request.BookId,
            StockTransactionId = stockTransaction.Id,
            Count = request.Count,
        };
        await appDbContext.AddAsync(OrderDetail, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);

        if (stock == request.Count)
        {
            book.OutOfStock = true;
            appDbContext.Update<Book>(book);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }

        return new CreateOrderCommandResult
        {
            Id = order.Id,
            CreatedDateTime = order.CreatedDateTime,
            OrderStatus = order.OrderStatus,
            BookId = OrderDetail.BookId,
            Count = OrderDetail.Count,
            BookTitle = OrderDetail.Book.Title,
            BookPrice = OrderDetail.Book.Price,
            TotalPrice = OrderDetail.Book.Price * OrderDetail.Count,
        };

    }
}