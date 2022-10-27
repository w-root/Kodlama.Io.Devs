using Application.Features.OperationClaimFeature.Commands.DeleteOperationClaim;
using Application.Features.OperationClaimFeature.Dtos;
using Application.Features.OperationClaimFeature.Rules;
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
            OperationClaimsRules _operationClaimsRules;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper,
                OperationClaimsRules operationClaimsRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimsRules = operationClaimsRules;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(g => g.Id == request.Id);

                _operationClaimsRules.OperationClaimShouldExistWhenRequested(operationClaim);

                operationClaim.Name = request.Name;
                OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);
                UpdatedOperationClaimDto mappedOperationClaim = _mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);
                return mappedOperationClaim;

            }
        }
    }
}
