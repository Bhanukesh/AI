namespace ApiService.Users.Commands;

using MediatR;
using Data;

public record UpdateUserDto(bool? IsNewUser, int? HistoryBiteIndex, DateTime? LastSentAt);

public record UpdateUserCommand(Guid Id, UpdateUserDto Dto) : IRequest;

public class UpdateUserHandler(TodoDbContext context) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync([request.Id], cancellationToken);
        if (user is null) return;

        if (request.Dto.IsNewUser.HasValue)
            user.IsNewUser = request.Dto.IsNewUser.Value;

        if (request.Dto.HistoryBiteIndex.HasValue)
            user.HistoryBiteIndex = request.Dto.HistoryBiteIndex.Value;

        if (request.Dto.LastSentAt.HasValue)
            user.LastSentAt = request.Dto.LastSentAt.Value;

        await context.SaveChangesAsync(cancellationToken);
    }
}
