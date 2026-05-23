namespace ApiService.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Brief.Commands;
using Brief.DTO;

[ApiController]
[Route("[controller]")]
public class BriefController(IMediator mediator) : ControllerBase
{
    [HttpPost("generate", Name = nameof(GenerateBrief))]
    public async Task<ActionResult<BriefResponse>> GenerateBrief(BriefRequest request)
        => await mediator.Send(new GenerateBriefCommand(request.UserId));

    [HttpPost("onboarding", Name = nameof(GenerateOnboarding))]
    public async Task<ActionResult<BriefResponse>> GenerateOnboarding(BriefRequest request)
        => await mediator.Send(new GenerateOnboardingCommand(request.UserId));
}
