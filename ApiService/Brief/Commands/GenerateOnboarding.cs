namespace ApiService.Brief.Commands;

using MediatR;
using Data;
using Brief.DTO;

public record GenerateOnboardingCommand(Guid UserId) : IRequest<BriefResponse>;

public class GenerateOnboardingHandler(TodoDbContext context, BriefClient briefClient) : IRequestHandler<GenerateOnboardingCommand, BriefResponse>
{
    public async Task<BriefResponse> Handle(GenerateOnboardingCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync([request.UserId], cancellationToken)
            ?? throw new KeyNotFoundException($"User {request.UserId} not found");

        return await briefClient.GenerateOnboardingAsync(user.Id);
    }
}
