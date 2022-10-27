using Application.Features.OperationClaimFeature.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaimFeature.Queries.GetOperationClaimById
{
    public class GetOperationClaimByIdQuery : IRequest<GetOperationClaimByIdDto>
    {
        public int Id { get; set; }
        public class GetOperationClaimByIdQueryHandler : IRequestHandler<GetOperationClaimByIdQuery, GetOperationClaimByIdDto>
        {
            public IOperationClaimRepository _operationClaimRepository;
            public IMapper _mapper;

            public GetOperationClaimByIdQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<GetOperationClaimByIdDto> Handle(GetOperationClaimByIdQuery request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = await _operationClaimRepository.GetAsync(c => c.Id == request.Id);
                GetOperationClaimByIdDto mappedOperationClaim = _mapper.Map<GetOperationClaimByIdDto>(operationClaim);
                return mappedOperationClaim;
            }
        }
    }
}
