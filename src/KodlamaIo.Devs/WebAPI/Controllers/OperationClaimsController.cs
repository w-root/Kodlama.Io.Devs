using Application.Features.OperationClaimFeature.Commands.CreateOperationClaim;
using Application.Features.OperationClaimFeature.Commands.DeleteOperationClaim;
using Application.Features.OperationClaimFeature.Commands.UpdateOperationClaim;
using Application.Features.OperationClaimFeature.Dtos;
using Application.Features.OperationClaimFeature.Models;
using Application.Features.OperationClaimFeature.Queries.GetListOperationClaim;
using Application.Features.OperationClaimFeature.Queries.GetOperationClaimById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListOperationClaimQuery getListOperationClaimQuery)
        {
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetOperationClaimByIdQuery getOperationClaimByIdQuery)
        {
            GetOperationClaimByIdDto result = await Mediator.Send(getOperationClaimByIdQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreatedOperationClaimDto result = await Mediator.Send(createOperationClaimCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeletedOperationClaimDto result = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            UpdatedOperationClaimDto result = await Mediator.Send(updateOperationClaimCommand);
            return Ok(result);
        }

    }
}
