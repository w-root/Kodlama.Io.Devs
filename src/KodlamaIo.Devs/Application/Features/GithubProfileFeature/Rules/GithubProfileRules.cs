using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfileFeature.Rules
{
    public class GithubProfileRules
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGithubProfileRepository _githubProfileRepository;
        public GithubProfileRules(IGithubProfileRepository githubProfileRepository, IHttpContextAccessor httpContextAccessor)
        {
            _githubProfileRepository = githubProfileRepository;
            _httpContextAccessor = httpContextAccessor;

        }
        public int GetUserId()
        {
            int id = _httpContextAccessor.HttpContext.User.GetUserId();
            return id;
        }

        public async Task GithubProfileCanNotBeDuplicatedWhenCreated(int userId)
        {
            GithubProfile githubProfile = await _githubProfileRepository.GetAsync(g => g.UserId == userId);
            if (githubProfile != null) throw new BusinessException("You already have a github profile.");
        }        
        
        public void GithubProfileShouldExistWhenRequested(GithubProfile githubProfile)
        {
            if (githubProfile == null) throw new BusinessException("Github profile does not exist.");
        }        
        
        public void UserCanOnlyUpdateAndDeleteGithubAddresTheyOwn(GithubProfile githubProfile)
        {
            if (githubProfile.UserId != GetUserId()) throw new BusinessException("You can only update or delete your own github profile.");
        }

    }
}
