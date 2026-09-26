using Application.Features.Authentication.Commands.ChangePassword;
using Application.Features.Authentication.Commands.ConfirmEmail;
using Application.Features.Authentication.Commands.ForgotPassword;
using Application.Features.Authentication.Commands.Login;
using Application.Features.Authentication.Commands.Logout;
using Application.Features.Authentication.Commands.Register;
using Application.Features.Authentication.Commands.ResetPassword;
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
        private readonly ISender _sender;
        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register( RegisterCommand register)
        {
            var result = await _sender.Send(register);

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

            var result = await _sender.Send(command);
            
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _sender.Send(command);
            return Ok(result);
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout ( LogoutCommand command)
        {
            await _sender.Send(command);

            return Ok(new{ message = "Logged out successfully." });
        }
        [HttpPost("FotgotPassword")]
        public async Task<IActionResult>ForgotPassword ( ForgorPasswordCommand command )
        {
            var result = await _sender.Send(command);
            return Ok(result);
        }
        [HttpPost("ResetPasswword")]
        public async Task<IActionResult>ResetPassword ( ResetPasswordCommand command)
        {
            var result = await _sender.Send(command);

            return Ok(result);
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult>ChangePassword (ChangePasswordCommand command)
        {
            var result = _sender.Send(command);

            return Ok(result);
        }
    }
}
