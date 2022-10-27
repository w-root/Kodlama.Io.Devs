using Application.Features.UserOperationClaimFeature.Dtos;
using Application.Features.UserOperationClaimFeature.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaimFeature.Queries.GetListUserOperationClaim
{
    public class GetListUserOperationClaimQuery : IRequest<UserOperationClaimListModel>
    {
        public int UserId { get; set; }
        public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, UserOperationClaimListModel>
        {
            IUserOperationClaimRepository _userOperationClaimRepository;
            IMapper _mapper;

            public GetListUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.
                    GetListAsync(u => u.UserId == request.UserId, include: a => a.Include(m => m.OperationClaim).Include(c => c.User));

                UserOperationClaimListModel mappedUserOperationClaims = _mapper.Map<UserOperationClaimListModel>(userOperationClaims);
                return mappedUserOperationClaims;
            }
        }
    }
}
