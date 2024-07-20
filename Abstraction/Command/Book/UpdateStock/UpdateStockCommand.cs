using MediatR;

namespace Abstraction.Command.Book.UpdateStock;

public class UpdateStockCommand : IRequest<UpdateStockCommandResult>
{
    public int Id { get; set; }
    public int Stock { get; set; }
}
