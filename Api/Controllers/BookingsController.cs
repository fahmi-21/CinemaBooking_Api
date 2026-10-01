using Api.Contracts.Bookings;
using Application.Common.Constants;
using Application.Features.Bookings.Commands.Create;
using Application.Features.Bookings.Commands.Delete;
using Application.Features.Bookings.Commands.Update;
using Application.Features.Bookings.Queries.GetBookings;
using Application.Features.Bookings.Queries.GetBookingById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class BookingsController : ControllerBase
{
    private readonly ISender _sender;

    public BookingsController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingCommand command)
        => Ok(await _sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _sender.Send(new GetBookingsQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetBookingByIdQuery(id));
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookingRequest request)
    {
        var command = new UpdateBookingCommand(
            id,
            request.UserId,
            request.ShowtimeId,
            request.Status,
            request.TotalAmount,
            request.ExpiresAt,
            request.ConfirmedAt);
        var result = await _sender.Send(command);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteBookingCommand(id));
        return result.Success ? Ok(result) : NotFound(result);
    }
}
