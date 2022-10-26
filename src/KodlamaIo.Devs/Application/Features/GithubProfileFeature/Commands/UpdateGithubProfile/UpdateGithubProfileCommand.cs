using Application.Features.GithubProfileFeature.Commands.DeleteGithubProfile;
using Application.Features.GithubProfileFeature.Dtos;
using Application.Features.GithubProfileFeature.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfileFeature.Commands.UpdateGithubProfile
{
    public class UpdateGithubProfileCommand : IRequest<UpdatedGithubProfileDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Github { get; set; }
        public string[] Roles => new[] { "User" };

        public class UpdateGithubProfileCommandHandler : IRequestHandler<UpdateGithubProfileCommand, UpdatedGithubProfileDto>
        {
            private readonly IMapper _mapper;
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly GithubProfileRules _githubProfileRules;

            public UpdateGithubProfileCommandHandler(IMapper mapper, IGithubProfileRepository githubProfileRepository,
                GithubProfileRules githubProfileRules)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _githubProfileRules = githubProfileRules;
            }
            public async Task<UpdatedGithubProfileDto> Handle(UpdateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile? githubProfile = await _githubProfileRepository.GetAsync(g => g.Id == request.Id);

                _githubProfileRules.UserCanOnlyUpdateAndDeleteGithubAddresTheyOwn(githubProfile);
                _githubProfileRules.GithubProfileShouldExistWhenRequested(githubProfile);

                githubProfile.Github = request.Github;
                GithubProfile updatedGithubProfile = await _githubProfileRepository.UpdateAsync(githubProfile);
                UpdatedGithubProfileDto updatedGithubProfileDto = _mapper.Map<UpdatedGithubProfileDto>(updatedGithubProfile);
                return updatedGithubProfileDto;
            }
        }
    }
}
