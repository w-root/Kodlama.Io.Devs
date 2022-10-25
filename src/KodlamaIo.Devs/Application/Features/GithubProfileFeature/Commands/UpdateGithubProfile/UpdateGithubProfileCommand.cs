using Application.Features.GithubProfileFeature.Commands.DeleteGithubProfile;
using Application.Features.GithubProfileFeature.Dtos;
using Application.Services.Repositories;
using AutoMapper;
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
    public class UpdateGithubProfileCommand : IRequest<UpdatedGithubProfileDto>
    {
        public int Id { get; set; }
        public string Github { get; set; }
        public class UpdateGithubProfileCommandHandler : IRequestHandler<UpdateGithubProfileCommand, UpdatedGithubProfileDto>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            IMapper _mapper;
            IGithubProfileRepository _githubProfileRepository;
            public UpdateGithubProfileCommandHandler(IMapper mapper, IGithubProfileRepository githubProfileRepository,
                IHttpContextAccessor httpContextAccessor)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<UpdatedGithubProfileDto> Handle(UpdateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile githubProfile = _mapper.Map<GithubProfile>(request);
                GithubProfile updatedGithubProfile = await _githubProfileRepository.UpdateAsync(githubProfile);
                UpdatedGithubProfileDto updatedGithubProfileDto = _mapper.Map<UpdatedGithubProfileDto>(updatedGithubProfile);
                return updatedGithubProfileDto;
            }
        }
    }
}
