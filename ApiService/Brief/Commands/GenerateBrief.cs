namespace ApiService.Brief.Commands;

using MediatR;
using Data;
using Brief.DTO;
using Microsoft.EntityFrameworkCore;

public record GenerateBriefCommand(Guid UserId) : IRequest<BriefResponse>;

public class GenerateBriefHandler(TodoDbContext context, BriefClient briefClient) : IRequestHandler<GenerateBriefCommand, BriefResponse>
{
    public async Task<BriefResponse> Handle(GenerateBriefCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync([request.UserId], cancellationToken)
            ?? throw new KeyNotFoundException($"User {request.UserId} not found");

        return await briefClient.GenerateBriefAsync(user.Id, user.HistoryBiteIndex);
    }
}
