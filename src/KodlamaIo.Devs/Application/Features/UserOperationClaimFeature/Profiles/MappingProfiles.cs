using Application.Features.UserOperationClaimFeature.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaimFeature.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaimFeature.Commands.UpdateUserOperationClaim;
using Application.Features.UserOperationClaimFeature.Dtos;
using Application.Features.UserOperationClaimFeature.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaimFeature.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, UpdatedUserOperationClaimDto>().ReverseMap();

            CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListModel>().ReverseMap();
            CreateMap<UserOperationClaim, GetListUserOperationClaimDto>()
                .ForMember(c => c.UserEmail, opt => opt.MapFrom(c => c.User.Email))
                .ForMember(c => c.OperationClaimName, opt => opt.MapFrom(c => c.OperationClaim.Name))
                .ReverseMap();


        }
    }
}
