using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAdress { get; set; }
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
        {
            private readonly IAuthService _authService;
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            public RegisterCommandHandler(IAuthService authService, IUserRepository userRepository,AuthBusinessRules authBusinessRules,
                IUserOperationClaimRepository userOperationClaimRepository)
            {
                _authService = authService;
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                byte[] passwordHash , passwordSalt;

                await _authBusinessRules.UserEmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);   

                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password,out passwordHash,out passwordSalt);
                User user = new()
                {
                    FirstName = request.UserForRegisterDto.FirstName,
                    Email = request.UserForRegisterDto.Email,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                };
                User addedUser = await _userRepository.AddAsync(user);

                UserOperationClaim userOperationClaim = new() { OperationClaimId = 1, UserId = addedUser.Id };
                await _userOperationClaimRepository.AddAsync(userOperationClaim);

                AccessToken accessToken = await _authService.CreateAccessToken(user);
                RefreshToken refreshToken = await _authService.CreateRefreshToken(user, request.IpAdress);
                await _authService.AddRefreshToken(refreshToken);
                RegisteredDto registeredDto = new() { 
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                return registeredDto;
            }
        }
    }
}
