using Abstraction.Command;
using Abstraction.Command.Book.CreateBook;
using Abstraction.Command.Book.GetBooks;
using Abstraction.Command.Book.UpdateStock;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/book")]
public class BookController : ControllerBase
{
    private readonly IMediator mediator;

    public BookController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CreateBookCommandResult>> CreateBook([FromBody] CreateBookCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch]
    [Authorize]
    public async Task<ActionResult<UpdateStockCommandResult>> UpdateStock([FromBody] UpdateStockCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]    
    public async Task<ActionResult<PagingResult<GetBooksQueryResult>>> GetBooks([FromQuery] int pageindex = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetBooksQuery() { PageIndex = pageindex, PageSize = pageSize };
        var books = await mediator.Send(query);
        return Ok(books);
    }


}