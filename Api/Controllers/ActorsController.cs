using Api.Contracts.Actors;
using Application.Common.Constants;
using Application.Features.Actors.Commands.Create;
using Application.Features.Actors.Commands.Delete;
using Application.Features.Actors.Commands.Update;
using Application.Features.Actors.Queries.GetActors;
using Application.Features.Actors.Queries.GetActorById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class ActorsController : ControllerBase
{
    private readonly ISender _sender;

    public ActorsController(ISender sender) => _sender = sender;

    [HttpPost]
    [Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE}")]
    public async Task<IActionResult> Create([FromBody] CreateActorCommand command)
        => Ok(await _sender.Send(command));

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
        => Ok(await _sender.Send(new GetActorsQuery()));

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetActorByIdQuery(id));
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateActorRequest request)
    {
        var command = new UpdateActorCommand(
            id,
            request.FullName,
            request.PhotoUrl);
        var result = await _sender.Send(command);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteActorCommand(id));
        return result.Success ? Ok(result) : NotFound(result);
    }
}
