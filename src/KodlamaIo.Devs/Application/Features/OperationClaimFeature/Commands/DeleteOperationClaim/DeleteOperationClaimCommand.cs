using Application.Features.OperationClaimFeature.Commands.CreateOperationClaim;
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

namespace Application.Features.OperationClaimFeature.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Roles => new[] { "Admin" };

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
        {
            IOperationClaimRepository _operationClaimRepository;
            IMapper _mapper;

            public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(operationClaim);
                DeletedOperationClaimDto mappedOperationClaim = _mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);
                return mappedOperationClaim;

            }
        }
    }
}
