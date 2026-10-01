using Api.Contracts.Payments;
using Application.Common.Constants;
using Application.Features.Payments.Commands.Create;
using Application.Features.Payments.Commands.Delete;
using Application.Features.Payments.Commands.Update;
using Application.Features.Payments.Queries.GetPayments;
using Application.Features.Payments.Queries.GetPaymentById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.ADMIN_ROLE)]
public sealed class PaymentsController : ControllerBase
{
    private readonly ISender _sender;

    public PaymentsController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
        => Ok(await _sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _sender.Send(new GetPaymentsQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetPaymentByIdQuery(id));
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePaymentRequest request)
    {
        var command = new UpdatePaymentCommand(
            id,
            request.BookingId,
            request.Amount,
            request.Method,
            request.Status,
            request.IdempotencyKey,
            request.ProviderReference,
            request.ProcessedAt);
        var result = await _sender.Send(command);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeletePaymentCommand(id));
        return result.Success ? Ok(result) : NotFound(result);
    }
}
