using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth
{
    public class AuthManager : IAuthService
    {
        IRefreshTokenRepository _refreshTokenRepository;
        ITokenHelper _tokenHelper;
        IUserOperationClaimRepository _userOperationClaimRepository;
        public AuthManager(IRefreshTokenRepository refreshTokenRepository, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenHelper = tokenHelper;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            return addedRefreshToken;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.
                GetListAsync(o => o.UserId == user.Id);

            IList<OperationClaim> operationClaims = userOperationClaims.Items.
                Select(u => new OperationClaim { Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();
            
            AccessToken token = _tokenHelper.CreateToken(user,operationClaims);
            return token;
        }

        public Task<RefreshToken> CreateRefreshToken(User user, string ipAdress)
        {
            RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAdress);
            return Task.FromResult(refreshToken);
        }
    }
}
