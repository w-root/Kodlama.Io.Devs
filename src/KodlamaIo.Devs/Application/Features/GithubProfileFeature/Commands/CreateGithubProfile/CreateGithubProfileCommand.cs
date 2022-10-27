using Application.Features.GithubProfileFeature.Dtos;
using Application.Features.GithubProfileFeature.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfileFeature.Commands.CreateGithubProfile
{
    public class CreateGithubProfileCommand : IRequest<CreatedGithubProfileDto>, ISecuredRequest
    {
        public string Github { get; set; }
        public string[] Roles => new[] { "User" };


        public class CreateGithubProfileCommandHandler : IRequestHandler<CreateGithubProfileCommand, CreatedGithubProfileDto>
        {
            private readonly IMapper _mapper;
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly GithubProfileRules _githubProfileRules;

            public CreateGithubProfileCommandHandler(IMapper mapper,IGithubProfileRepository githubProfileRepository,
                GithubProfileRules githubProfileRules)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _githubProfileRules = githubProfileRules;
            }
            public async Task<CreatedGithubProfileDto> Handle(CreateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                int userId = _githubProfileRules.GetUserId();
                await _githubProfileRules.GithubProfileCanNotBeDuplicatedWhenCreated(userId);

                GithubProfile githubProfile = _mapper.Map<GithubProfile>(request);
                githubProfile.UserId = userId;

                GithubProfile addedGithubProfile = await _githubProfileRepository.AddAsync(githubProfile);
                CreatedGithubProfileDto createdGithubProfileDto = _mapper.Map<CreatedGithubProfileDto>(addedGithubProfile);
                return createdGithubProfileDto;
            }
        }
    }
}
