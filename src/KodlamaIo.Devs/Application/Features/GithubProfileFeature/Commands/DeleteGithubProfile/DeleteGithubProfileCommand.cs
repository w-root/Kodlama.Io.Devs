using Application.Features.GithubProfileFeature.Commands.CreateGithubProfile;
using Application.Features.GithubProfileFeature.Dtos;
using Application.Features.GithubProfileFeature.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfileFeature.Commands.DeleteGithubProfile
{
    public class DeleteGithubProfileCommand : IRequest<DeletedGithubProfileDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Github { get; set; }
        public string[] Roles => new[] { "User" };

        public class DeleteGithubProfileCommandHandler : IRequestHandler<DeleteGithubProfileCommand, DeletedGithubProfileDto>
        {
            private readonly IMapper _mapper;
            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly GithubProfileRules _githubProfileRules;

            public DeleteGithubProfileCommandHandler(IMapper mapper, IGithubProfileRepository githubProfileRepository,
                 GithubProfileRules githubProfileRules)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _githubProfileRules = githubProfileRules;
            }
            public async Task<DeletedGithubProfileDto> Handle(DeleteGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile? githubProfile = await _githubProfileRepository.GetAsync(g => g.Id == request.Id);

                _githubProfileRules.UserCanOnlyUpdateAndDeleteGithubAddresTheyOwn(githubProfile);
                _githubProfileRules.GithubProfileShouldExistWhenRequested(githubProfile);

                GithubProfile deletedGithubProfile = await _githubProfileRepository.DeleteAsync(githubProfile);
                DeletedGithubProfileDto deletedGithubProfileDto = _mapper.Map<DeletedGithubProfileDto>(deletedGithubProfile);
                return deletedGithubProfileDto;
            }
        }
    }
}
