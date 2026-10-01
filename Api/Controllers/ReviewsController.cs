using Api.Contracts.Reviews;
using Application.Common.Constants;
using Application.Features.Reviews.Commands.Create;
using Application.Features.Reviews.Commands.Delete;
using Application.Features.Reviews.Commands.Update;
using Application.Features.Reviews.Queries.GetReviews;
using Application.Features.Reviews.Queries.GetReviewById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class ReviewsController : ControllerBase
{
    private readonly ISender _sender;

    public ReviewsController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
        => Ok(await _sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _sender.Send(new GetReviewsQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetReviewByIdQuery(id));
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewRequest request)
    {
        var command = new UpdateReviewCommand(
            id,
            request.UserId,
            request.MovieId,
            request.Rating,
            request.Comment,
            request.IsApproved);
        var result = await _sender.Send(command);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteReviewCommand(id));
        return result.Success ? Ok(result) : NotFound(result);
    }
}
