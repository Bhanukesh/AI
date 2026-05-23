namespace ApiService.Delivery.Commands;

using MediatR;
using Data;

public record LogDeliveryCommand(
    Guid UserId,
    string Channel,
    string Status,
    string BriefSummary) : IRequest;

public class LogDeliveryHandler(TodoDbContext context) : IRequestHandler<LogDeliveryCommand>
{
    public async Task Handle(LogDeliveryCommand request, CancellationToken cancellationToken)
    {
        var log = new DeliveryLog
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Date = DateTime.UtcNow.Date,
            Channel = request.Channel,
            Status = request.Status,
            BriefSummary = request.BriefSummary.Length > 200
                ? request.BriefSummary[..200]
                : request.BriefSummary,
            CreatedAt = DateTime.UtcNow
        };

        context.DeliveryLogs.Add(log);
        await context.SaveChangesAsync(cancellationToken);
    }
}
