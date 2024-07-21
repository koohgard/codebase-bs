using Abstraction.Enum;
using MediatR;

namespace Abstraction.Command.Book.UpdateStock;

public class UpdateStockCommand : IRequest<UpdateStockCommandResult>
{
    public int BookId { get; set; }
    public int Count{ get; set; }
    public TransactionFactor TransactionFactor { get; set; }
}
