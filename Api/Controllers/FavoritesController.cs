using Api.Contracts.Favorites;
using Application.Common.Constants;
using Application.Features.Favorites.Commands.Create;
using Application.Features.Favorites.Commands.Delete;
using Application.Features.Favorites.Commands.Update;
using Application.Features.Favorites.Queries.GetFavorites;
using Application.Features.Favorites.Queries.GetFavoriteById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class FavoritesController : ControllerBase
{
    private readonly ISender _sender;

    public FavoritesController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFavoriteCommand command)
        => Ok(await _sender.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _sender.Send(new GetFavoritesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _sender.Send(new GetFavoriteByIdQuery(id));
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateFavoriteRequest request)
    {
        var command = new UpdateFavoriteCommand(
            id,
            request.UserId,
            request.MovieId);
        var result = await _sender.Send(command);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _sender.Send(new DeleteFavoriteCommand(id));
        return result.Success ? Ok(result) : NotFound(result);
    }
}
