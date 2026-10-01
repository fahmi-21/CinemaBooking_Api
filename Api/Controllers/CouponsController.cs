using Api.Contracts.Coupons;
using Application.Common.Constants;
using Application.Features.Coupons.Commands.Create;
using Application.Features.Coupons.Commands.Delete;
using Application.Features.Coupons.Commands.Update;
using Application.Features.Coupons.Queries.GetCoupons;
using Application.Features.Coupons.Queries.GetCouponById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class CouponsController : ControllerBase
{
    private readonly ISender _sender;

    public CouponsController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCouponCommand command)
        => Ok(await _sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _sender.Send(new GetCouponsQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetCouponByIdQuery(id));
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCouponRequest request)
    {
        var command = new UpdateCouponCommand(
            id,
            request.Code,
            request.DiscountType,
            request.Value,
            request.MinOrderAmount,
            request.MaxUsageCount,
            request.UsedCount,
            request.ExpiryDate,
            request.IsActive,
            request.ApplicableMovieId,
            request.ApplicableBranchId);
        var result = await _sender.Send(command);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteCouponCommand(id));
        return result.Success ? Ok(result) : NotFound(result);
    }
}
