using Application.Features.TechnologyFeature.Commands.CreateTechnologyCommand;
using Application.Features.TechnologyFeature.Commands.DeleteTechnologyCommand;
using Application.Features.TechnologyFeature.Commands.UpdateTechnologyCommand;
using Application.Features.TechnologyFeature.Dtos;
using Application.Features.TechnologyFeature.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TechnologyFeature.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, DeleteTechnologyCommand>().ReverseMap();
            CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();
            CreateMap<Technology, TechnologyListDto>()
                .ForMember(c => c.ProgrammingLanguageId, opt => opt.MapFrom(c => c.ProgrammingLanguage.Id))
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();
            
            CreateMap<Technology, TechnologyGetByIdDto>()
                .ForMember(c => c.ProgrammingLanguageId, opt => opt.MapFrom(c => c.ProgrammingLanguage.Id))
                .ForMember(c => c.ProgramminLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();


        }
    }
}

