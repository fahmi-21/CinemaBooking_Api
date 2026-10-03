using Api.Contracts.Branches;
using Application.Common;
using Application.Common.Constants;
using Application.Common.Models;
using Application.Features.Branches.Commands.Create;
using Application.Features.Branches.Commands.Delete;
using Application.Features.Branches.Commands.Update;
using Application.Features.Branches.Queries.GetBranches;
using Application.Features.Branches.Queries.GetBranshById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE},{Roles.CUSTOMER_ROLE}")]

public class BranchesController : ControllerBase
{
    private readonly ISender _sender;

    public BranchesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE}")]
    public async Task<IActionResult> Create(CreateBranchCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteBranchCommand(id));

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBranchRequest request)
    {
        var command = new UpdateBranchCommand(
            id,
            request.Name,
            request.Address,
            request.Latitude,
            request.Longitude,
            request.GoogleMapsUrl);
        var result = await _sender.Send(command);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetBranchByIdQuery(id));

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetBranchesQuery query)
    {
        var result = await _sender.Send(query);
        return Ok(result);
    }
}
