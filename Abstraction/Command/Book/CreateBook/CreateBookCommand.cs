using MediatR;

namespace Abstraction.Command.Book.CreateBook;

public class CreateBookCommand : IRequest<CreateBookCommandResult>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int InitStock { get; set; }
}
