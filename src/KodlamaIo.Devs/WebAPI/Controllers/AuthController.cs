using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Dtos;
using Core.Security.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new() { UserForRegisterDto=userForRegisterDto,IpAdress=GetIpAddress()};
            RegisteredDto registeredDto = await Mediator.Send(registerCommand);
            return Created("",registeredDto.AccessToken);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IpAdress = GetIpAddress() };
            LoggedInDto loggedInDto = await Mediator.Send(loginCommand);
            return Created("", loggedInDto.AccessToken);
        }
    }
}
