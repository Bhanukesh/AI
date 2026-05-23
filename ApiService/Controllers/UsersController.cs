namespace ApiService.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Commands;
using Users.Queries;
using Users.DTO;

[ApiController]
[Route("[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = nameof(GetUsers))]
    public async Task<IEnumerable<UserItem>> GetUsers()
        => await mediator.Send(new GetUsersQuery());

    [HttpPost("register", Name = nameof(RegisterUser))]
    public async Task<ActionResult<Guid>> RegisterUser(RegisterUserCommand command)
        => await mediator.Send(command);

    [HttpPut("{id}", Name = nameof(UpdateUser))]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto dto)
    {
        await mediator.Send(new UpdateUserCommand(id, dto));
        return NoContent();
    }
}
