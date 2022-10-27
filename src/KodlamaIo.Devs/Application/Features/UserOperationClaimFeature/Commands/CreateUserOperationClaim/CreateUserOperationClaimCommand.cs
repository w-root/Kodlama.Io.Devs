using Application.Features.UserOperationClaimFeature.Dtos;
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

namespace Application.Features.UserOperationClaimFeature.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimDto>, ISecuredRequest
    {
        public int OperationClaimId { get; set; }
        public int UserId { get; set; }

        public string[] Roles => new[] { "Admin" };

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
        {
            IUserOperationClaimRepository _userOperationClaimRepository;
            IMapper _mapper;

            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim addedUserOperationClaim = await _userOperationClaimRepository.AddAsync(userOperationClaim);
                CreatedUserOperationClaimDto mappedUserOperationClaimDto = _mapper.Map<CreatedUserOperationClaimDto>(addedUserOperationClaim);
                return mappedUserOperationClaimDto;
            }
        }
    }
}
