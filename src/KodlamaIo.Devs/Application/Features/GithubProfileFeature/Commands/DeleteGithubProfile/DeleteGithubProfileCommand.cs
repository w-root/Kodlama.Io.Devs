using Application.Features.GithubProfileFeature.Commands.CreateGithubProfile;
using Application.Features.GithubProfileFeature.Dtos;
using Application.Services.Repositories;
using AutoMapper;
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
    public class DeleteGithubProfileCommand : IRequest<DeletedGithubProfileDto>
    {
        public int Id { get; set; }
        public string Github { get; set; }

        public class DeleteGithubProfileCommandHandler : IRequestHandler<DeleteGithubProfileCommand, DeletedGithubProfileDto>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            IMapper _mapper;
            IGithubProfileRepository _githubProfileRepository;
            public DeleteGithubProfileCommandHandler(IMapper mapper, IGithubProfileRepository githubProfileRepository,
                IHttpContextAccessor httpContextAccessor)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<DeletedGithubProfileDto> Handle(DeleteGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile githubProfile = _mapper.Map<GithubProfile>(request);
                GithubProfile deletedGithubProfile = await _githubProfileRepository.DeleteAsync(githubProfile);
                DeletedGithubProfileDto deletedGithubProfileDto = _mapper.Map<DeletedGithubProfileDto>(deletedGithubProfile);
                return deletedGithubProfileDto;
            }
        }
    }
}
