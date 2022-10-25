using Application.Features.GithubProfileFeature.Dtos;
using Application.Services.Repositories;
using AutoMapper;
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
    public class CreateGithubProfileCommand : IRequest<CreatedGithubProfileDto>
    {
        public string Github { get; set; }

        public class CreateGithubProfileCommandHandler : IRequestHandler<CreateGithubProfileCommand, CreatedGithubProfileDto>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            IMapper _mapper;
            IGithubProfileRepository _githubProfileRepository;
            public CreateGithubProfileCommandHandler(IMapper mapper,IGithubProfileRepository githubProfileRepository, 
                IHttpContextAccessor httpContextAccessor)
            {
                _mapper = mapper;
                _githubProfileRepository = githubProfileRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<CreatedGithubProfileDto> Handle(CreateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile githubProfile = _mapper.Map<GithubProfile>(request);
                int id = _httpContextAccessor.HttpContext.User.GetUserId();
                if(id == 0)
                    throw new BusinessException("Please Login");

                githubProfile.UserId = id;

                GithubProfile addedGithubProfile = await _githubProfileRepository.AddAsync(githubProfile);
                CreatedGithubProfileDto createdGithubProfileDto = _mapper.Map<CreatedGithubProfileDto>(addedGithubProfile);
                return createdGithubProfileDto;
            }
        }
    }
}
