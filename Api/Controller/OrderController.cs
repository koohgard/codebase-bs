using Abstraction.Command;
using Abstraction.Command.Order.CreateOrder;
using Abstraction.Command.Order.GetOrder;
using Abstraction.Command.Order.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IMediator mediator;

    public OrderController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateOrderCommandResult>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetOrderQueryResult>> GetOrder(int id)
    {
        var query = new GetOrderQuery() { Id = id };
        var order = await mediator.Send(query);
        return Ok(order);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagingResult<GetOrdersQueryResult>>> GetOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int pageindex = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetOrdersQuery()
        {
            PageIndex = pageindex,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };
        var orders = await mediator.Send(query);
        return Ok(orders);
    }


}