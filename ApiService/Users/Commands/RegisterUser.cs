namespace ApiService.Users.Commands;

using MediatR;
using Data;

public record RegisterUserCommand(string Name, string Email, string WhatsAppNumber) : IRequest<Guid>;

public class RegisterUserHandler(TodoDbContext context) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            WhatsAppNumber = request.WhatsAppNumber,
            IsNewUser = true,
            HistoryBiteIndex = 0,
            DateJoined = DateTime.UtcNow
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}
