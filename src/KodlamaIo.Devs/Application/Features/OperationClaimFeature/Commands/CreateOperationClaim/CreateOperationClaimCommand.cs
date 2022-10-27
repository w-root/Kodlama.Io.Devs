using Application.Features.OperationClaimFeature.Dtos;
using Application.Features.OperationClaimFeature.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaimFeature.Commands.CreateOperationClaim
{
    public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto>, ISecuredRequest
    {
        public string Name { get; set; }

        public string[] Roles => new[] { "Admin" };

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {
            IOperationClaimRepository _operationClaimRepository;
            IMapper _mapper;
            OperationClaimsRules _operationClaimsRules;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper,
                OperationClaimsRules operationClaimsRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimsRules = operationClaimsRules;
            }

            public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimsRules.OperationClaimNameCanNotBeDuplicatedWhenCreated(request.Name);

                OperationClaim operationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim createdoperationClaim = await _operationClaimRepository.AddAsync(operationClaim);
                CreatedOperationClaimDto mappedOperationClaim = _mapper.Map<CreatedOperationClaimDto>(createdoperationClaim);
                return mappedOperationClaim;

            }
        }
    }
}
