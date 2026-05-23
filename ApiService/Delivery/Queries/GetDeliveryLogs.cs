namespace ApiService.Delivery.Queries;

using MediatR;
using Data;
using Delivery.DTO;
using Microsoft.EntityFrameworkCore;

public record GetDeliveryLogsQuery(Guid? UserId, bool Latest) : IRequest<IEnumerable<DeliveryLogItem>>;

public class GetDeliveryLogsHandler(TodoDbContext context) : IRequestHandler<GetDeliveryLogsQuery, IEnumerable<DeliveryLogItem>>
{
    public async Task<IEnumerable<DeliveryLogItem>> Handle(GetDeliveryLogsQuery request, CancellationToken cancellationToken)
    {
        var query = context.DeliveryLogs
            .Include(d => d.User)
            .AsQueryable();

        if (request.UserId.HasValue)
            query = query.Where(d => d.UserId == request.UserId.Value);

        query = query.OrderByDescending(d => d.CreatedAt);

        if (request.Latest)
            query = query.Take(1);

        return await query
            .Select(d => new DeliveryLogItem(
                d.Id, d.UserId, d.User.Email,
                d.Date, d.Channel, d.Status, d.BriefSummary, d.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
