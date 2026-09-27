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
public class BranchesController : ControllerBase
{
    private readonly ISender _sender;

    public BranchesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("CreateBranch")]
    [Authorize(Application.Common.Constants.Roles.ADMIN_ROLE)]
    public async Task<IActionResult> Create( CreateBranchCommand command)
    {
        var branchId = await _sender.Send(command);

        return Ok(new
        {
            id = branchId,
            message = "Branch Created Successfully"
        });
    }

    [HttpDelete("DeleteBranch")]
    [Authorize(Application.Common.Constants.Roles.ADMIN_ROLE)]
    public async Task<IActionResult> Delete(DeleteCommand command)
    {
        var result = await _sender.Send(
            new DeleteCommand(command.Id));

        if (!result)
        {
            return NotFound(new
            {
                message = "Branch not found."
            });
        }

        return Ok(new
        {
            message = "Branch Deleted Successfully"
        });
    }

    [HttpPut("UpdateBranch")]
    [Authorize(Application.Common.Constants.Roles.ADMIN_ROLE)]
    public async Task<IActionResult> Update(int id,  UpdateCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new
            {
                message = "Route ID and request ID must match."
            });
        }

        var result = await _sender.Send(command);

        if (!result)
        {
            return NotFound(new
            {
                message = "Branch not found."
            });
        }

        return Ok(new
        {
            message = "Branch updated successfully."
        });
    }

    [HttpGet("GetBranchById")]
    [Authorize(Application.Common.Constants.Roles.ADMIN_ROLE)]
    public async Task<IActionResult> GetById( [FromQuery] GetBranchByIdQuery query)
    {
        var result = await _sender.Send(query);

        if (result is null)
        {
            return NotFound(new
            {
                message = "Branch not found."
            });
        }

        return Ok(result);
    }

    [HttpGet("GetBranches")]
    [Authorize(Application.Common.Constants.Roles.ADMIN_ROLE)]
    public async Task<IActionResult> GetAll( [FromQuery] GetBranchesQuery query)
    {
        var result = await _sender.Send(query);

        return Ok(result);
    }
}