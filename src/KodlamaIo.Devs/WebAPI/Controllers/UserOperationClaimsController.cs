using Application.Features.UserOperationClaimFeature.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaimFeature.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaimFeature.Commands.UpdateUserOperationClaim;
using Application.Features.UserOperationClaimFeature.Dtos;
using Application.Features.UserOperationClaimFeature.Models;
using Application.Features.UserOperationClaimFeature.Queries.GetListUserOperationClaim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : BaseController
    {
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetById([FromRoute] GetListUserOperationClaimQuery getListUserOperationClaimQuery)
        {
            UserOperationClaimListModel result = await Mediator.Send(getListUserOperationClaimQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            CreatedUserOperationClaimDto result = await Mediator.Send(createUserOperationClaimCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
        {
            var result = await Mediator.Send(deleteUserOperationClaimCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
        {
            UpdatedUserOperationClaimDto result = await Mediator.Send(updateUserOperationClaimCommand);
            return Ok(result);
        }

    }
}
