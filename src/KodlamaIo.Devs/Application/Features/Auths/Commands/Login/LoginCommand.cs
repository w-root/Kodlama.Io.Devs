using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand:IRequest<LoggedInDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAdress{ get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand,LoggedInDto>
        {
            private readonly IAuthService _authService;
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;
            public LoginCommandHandler(IAuthService authService, IUserRepository userRepository,AuthBusinessRules authBusinessRules)
            {
                _authService = authService;
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;

            }
            public async Task<LoggedInDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);

                _authBusinessRules.UserEmailShouldExistWhenLoggedIn(user?.Email);
                _authBusinessRules.VerifyPassword(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken accessToken = await _authService.CreateAccessToken(user);
                RefreshToken refreshToken = await _authService.CreateRefreshToken(user, request.IpAdress);
                await _authService.AddRefreshToken(refreshToken);

                LoggedInDto loggedDto = new()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
                return loggedDto;
            }
        }
    }
}
