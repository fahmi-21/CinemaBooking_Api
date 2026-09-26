using Application.Features.Branches.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly ISender _sender;
        public BranchesController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create( CreateBranchCommand command)
        {
            var branchId = await _sender.Send(command);

            return Ok(new
            {
                id = branchId,
                message = "Branch Created Successfully"
            });
        }

    }
}
