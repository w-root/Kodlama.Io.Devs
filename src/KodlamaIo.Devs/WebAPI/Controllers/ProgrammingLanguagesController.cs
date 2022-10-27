using Application.Features.ProgrammingLanguageFeature.Dtos;
using Application.Features.ProgrammingLanguageFeature.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguageFeature.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguageFeature.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguageFeature.Models;
using Application.Features.ProgrammingLanguageFeature.Queries.GetByIdProgrammingLanguageQuery;
using Application.Features.ProgrammingLanguageFeature.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListProgrammingLanguage getListProgrammingLanguageQuery)
        {
            ProgrammingLanguageListModel result = await Mediator.Send(getListProgrammingLanguageQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
        {
            ProgrammingLanguageGetByIdDto result = await Mediator.Send(getByIdProgrammingLanguageQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
        {
            CreatedProgrammingLanguageDto result = await Mediator.Send(createProgrammingLanguageCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
        {
            DeletedProgrammingLanguageDto result = await Mediator.Send(deleteProgrammingLanguageCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
        {
           UpdatedProgrammingLanguageDto result = await Mediator.Send(updateProgrammingLanguageCommand);
            return Ok(result);
        }



    }
}
