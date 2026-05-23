namespace ApiService.Users.Queries;

using MediatR;
using Data;
using Users.DTO;
using Microsoft.EntityFrameworkCore;

public record GetUsersQuery : IRequest<IEnumerable<UserItem>>;

public class GetUsersHandler(TodoDbContext context) : IRequestHandler<GetUsersQuery, IEnumerable<UserItem>>
{
    public async Task<IEnumerable<UserItem>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
            .Select(u => new UserItem(
                u.Id, u.Name, u.Email, u.WhatsAppNumber,
                u.IsNewUser, u.HistoryBiteIndex, u.DateJoined, u.LastSentAt))
            .ToListAsync(cancellationToken);
    }
}
