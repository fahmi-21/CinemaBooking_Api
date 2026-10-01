using Api.Contracts.Showtimes;
using Application.Common.Constants;
using Application.Features.Showtimes.Commands.Create;
using Application.Features.Showtimes.Commands.Delete;
using Application.Features.Showtimes.Commands.Update;
using Application.Features.Showtimes.Queries.GetShowtimes;
using Application.Features.Showtimes.Queries.GetShowtimeById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE},{Roles.BRANCH_MANAGER_ROLE} , {Roles.EMPLOYEE_ROLE}")]
public sealed class ShowtimesController : ControllerBase
{
    private readonly ISender _sender;

    public ShowtimesController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShowtimeCommand command)
        => Ok(await _sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _sender.Send(new GetShowtimesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetShowtimeByIdQuery(id));
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateShowtimeRequest request)
    {
        var command = new UpdateShowtimeCommand(
            id,
            request.MovieId,
            request.HallId,
            request.Date,
            request.StartTime,
            request.EndTime,
            request.BasePrice,
            request.Status);
        var result = await _sender.Send(command);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteShowtimeCommand(id));
        return result.Success ? Ok(result) : NotFound(result);
    }
}
