using Api.Contracts.Halls;
using Application.Common.Constants;
using Application.Features.Halls.Commands.Create;
using Application.Features.Halls.Commands.Delete;
using Application.Features.Halls.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController ]
[Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE},{Roles.BRANCH_MANAGER_ROLE}")]
public class HallsController : ControllerBase
{
    private readonly ISender _sender;

    public HallsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHallCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Application.Common.Constants.Roles.ADMIN_ROLE)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateHallRequest request)
    {
        var command = new UpdateHallCommand(
            id,
            request.BranchId,
            request.Name,
            request.Type,
            request.Capacity,
            request.CleaningBufferMinutes);
        var result = await _sender.Send(command);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Application.Common.Constants.Roles.ADMIN_ROLE)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteHallCommand(id));

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
}
