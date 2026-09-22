using Application.Features.Authentication.Commands.ConfirmEmail;
using Application.Features.Authentication.Commands.Register;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender sender;
        public AuthController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register( RegisterCommand register)
        {
            var result = await sender.Send(register);

            if (result.UserId == Guid.Empty)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail ( [FromQuery] Guid userId ,[FromQuery] string token )
        {
            var command = new ConfirmEmailCommand( userId, token);

            var result = await sender.Send(command);
            
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
