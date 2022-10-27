using Application.Features.OperationClaimFeature.Commands.DeleteOperationClaim;
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

namespace Application.Features.OperationClaimFeature.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Roles => new[] { "Admin" };

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
        {
            IOperationClaimRepository _operationClaimRepository;
            IMapper _mapper;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);
                UpdatedOperationClaimDto mappedOperationClaim = _mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);
                return mappedOperationClaim;

            }
        }
    }
}
