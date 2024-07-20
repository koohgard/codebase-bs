// using GreenFlux.Abstraction.Command;
// using GreenFlux.Abstraction.Dto;
// using GreenFlux.Abstraction.Query;
// using MediatR;
using Abstraction;
using Abstraction.Command.Customer.CustomerOrders;
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

    [HttpGet("orders")]
    public async Task<ActionResult<IEnumerable<CustomerOrdersQueryResult>>> GetCustomerOrders([FromQuery] int pageindex = 1, [FromQuery] int pageSize = 10)
    {
        var query = new CustomerOrdersQuery() { PageIndex = pageindex, PageSize = pageSize };
        var groups = await mediator.Send(query);
        return Ok(groups);
    }


}