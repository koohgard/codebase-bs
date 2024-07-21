using Abstraction.Command;
using Abstraction.Command.Customer.GetCustomerOrders;
using Abstraction.Command.Customer.Login;
using Abstraction.Command.Customer.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly IMediator mediator;

    public CustomerController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterCommandResult>> Register([FromBody] RegisterCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginCommandResult>> Login([FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("orders")]
    public async Task<ActionResult<PagingResult<GetCustomerOrdersQueryResult>>> GetCustomerOrders([FromQuery] int pageindex = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetCustomerOrdersQuery() { PageIndex = pageindex, PageSize = pageSize };
        var orders = await mediator.Send(query);
        return Ok(orders);
    }


}