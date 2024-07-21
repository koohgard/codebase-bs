using Abstraction.Command.Order.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator mediator;

    public StatisticsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetOrderStatisticsQueryResult>>> GetOrders()
    {
        var query = new GetOrderStatisticsQuery() { };
        var data = await mediator.Send(query);
        return Ok(data);
    }


}