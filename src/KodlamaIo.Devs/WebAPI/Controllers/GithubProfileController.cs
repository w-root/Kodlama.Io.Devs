using Application.Features.GithubProfileFeature.Commands.CreateGithubProfile;
using Application.Features.GithubProfileFeature.Commands.DeleteGithubProfile;
using Application.Features.GithubProfileFeature.Commands.UpdateGithubProfile;
using Application.Features.GithubProfileFeature.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubProfileController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubProfileCommand createGithubProfileCommand)
        {
            CreatedGithubProfileDto result = await Mediator.Send(createGithubProfileCommand);
            return Created("", result);
        }        

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGithubProfileCommand updateGithubProfileCommand)
        {
            UpdatedGithubProfileDto result = await Mediator.Send(updateGithubProfileCommand);
            return Created("", result);
        }       

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGithubProfileCommand deleteGithubProfileCommand)
        {
            DeletedGithubProfileDto result = await Mediator.Send(deleteGithubProfileCommand);
            return Created("", result);
        }
    }
}
