namespace ApiService.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Delivery.Commands;
using Delivery.Queries;
using Delivery.DTO;

[ApiController]
[Route("[controller]")]
public class DeliveryController(IMediator mediator) : ControllerBase
{
    [HttpPost("log", Name = nameof(LogDelivery))]
    public async Task<IActionResult> LogDelivery(LogDeliveryCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpGet("logs", Name = nameof(GetDeliveryLogs))]
    public async Task<IEnumerable<DeliveryLogItem>> GetDeliveryLogs(
        [FromQuery] Guid? userId,
        [FromQuery] bool latest = false)
        => await mediator.Send(new GetDeliveryLogsQuery(userId, latest));
}
