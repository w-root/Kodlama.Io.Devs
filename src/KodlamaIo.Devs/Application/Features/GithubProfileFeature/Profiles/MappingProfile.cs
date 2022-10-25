using Application.Features.GithubProfileFeature.Commands.CreateGithubProfile;
using Application.Features.GithubProfileFeature.Commands.DeleteGithubProfile;
using Application.Features.GithubProfileFeature.Commands.UpdateGithubProfile;
using Application.Features.GithubProfileFeature.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfileFeature.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GithubProfile, CreatedGithubProfileDto>().ReverseMap();
            CreateMap<GithubProfile, CreateGithubProfileCommand>().ReverseMap();

            CreateMap<GithubProfile, UpdatedGithubProfileDto>().ReverseMap();
            CreateMap<GithubProfile, UpdateGithubProfileCommand>().ReverseMap();

            CreateMap<GithubProfile, DeletedGithubProfileDto>().ReverseMap();
            CreateMap<GithubProfile, DeleteGithubProfileCommand>().ReverseMap();
        }
    }
}
