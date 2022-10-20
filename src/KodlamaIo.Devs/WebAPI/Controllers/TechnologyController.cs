using Application.Features.TechnologyFeature.Commands.UpdateTechnologyCommand;
using Application.Features.TechnologyFeature.Commands.CreateTechnologyCommand;
using Application.Features.TechnologyFeature.Commands.DeleteTechnologyCommand;
using Application.Features.TechnologyFeature.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Application.Features.TechnologyFeature.Queries.GetListTechnologyQuery;
using Application.Features.TechnologyFeature.Queries.GetListTechnologyByDynamic;
using Application.Features.TechnologyFeature.Models;
using Application.Features.ProgrammingLanguageFeature.Dtos;
using Application.Features.ProgrammingLanguageFeature.Queries.GetByIdProgrammingLanguageQuery;
using Application.Features.TechnologyFeature.Queries.GetByIdTechnology;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListTechnologyQuery getListTechnologyQuery)
        {
            TechnologyListModel result = await Mediator.Send(getListTechnologyQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdTechnologyQuery getByIdTechnologyQuery)
        {
            TechnologyGetByIdDto result = await Mediator.Send(getByIdTechnologyQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListTechnologyByDynamicQuery getListTechnologyByDynamicQuery = 
                new GetListTechnologyByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };
            TechnologyListModel result = await Mediator.Send(getListTechnologyByDynamicQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            CreatedTechnologyDto result = await Mediator.Send(createTechnologyCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeletedTechnologyDto result = await Mediator.Send(deleteTechnologyCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdatedTechnologyDto result = await Mediator.Send(updateTechnologyCommand);
            return Ok(result);
        }
    }
}
