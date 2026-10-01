using Api.Contracts.Movies;
using Application.Common.Constants;
using Application.Features.Movies.Commands.Create;
using Application.Features.Movies.Commands.Delete;
using Application.Features.Movies.Commands.Update;
using Application.Features.Movies.Queries.GetMovies;
using Application.Features.Movies.Queries.GetMovieById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{Roles.ADMIN_ROLE},{Roles.SUPER_ADMIN_ROLE},{Roles.BRANCH_MANAGER_ROLE} , {Roles.EMPLOYEE_ROLE}")]
public sealed class MoviesController : ControllerBase
{
    private readonly ISender _sender;

    public MoviesController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovieCommand command)
        => Ok(await _sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _sender.Send(new GetMoviesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetMovieByIdQuery(id));
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMovieRequest request)
    {
        var command = new UpdateMovieCommand(
            id,
            request.Title,
            request.Description,
            request.DurationMinutes,
            request.ReleaseDate,
            request.PosterUrl,
            request.TrailerUrl,
            request.Language,
            request.Country,
            request.Director,
            request.AgeRating,
            request.AverageRating,
            request.Status);
        var result = await _sender.Send(command);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteMovieCommand(id));
        return result.Success ? Ok(result) : NotFound(result);
    }
}
