using MediatR;

namespace Abstraction.Command.Book.GetBooks;

public class GetBooksQuery : IRequest<PagingResult<GetBooksQueryResult>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
