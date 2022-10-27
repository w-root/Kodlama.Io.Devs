using Application.Features.OperationClaimFeature.Commands.CreateOperationClaim;
using Application.Features.OperationClaimFeature.Commands.DeleteOperationClaim;
using Application.Features.OperationClaimFeature.Commands.UpdateOperationClaim;
using Application.Features.OperationClaimFeature.Dtos;
using Application.Features.OperationClaimFeature.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaimFeature.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, CreatedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, DeletedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, UpdatedOperationClaimDto>().ReverseMap();

            CreateMap<OperationClaim, GetListOperationClaimDto>().ReverseMap();
            CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();
            CreateMap<OperationClaim, GetOperationClaimByIdDto>().ReverseMap();

        }
    }
}
