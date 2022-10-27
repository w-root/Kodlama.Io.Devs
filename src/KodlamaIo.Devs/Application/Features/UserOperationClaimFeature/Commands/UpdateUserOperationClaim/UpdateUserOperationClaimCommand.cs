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

namespace Application.Features.UserOperationClaimFeature.Commands.UpdateUserOperationClaim
{
    public class UpdateUserOperationClaimCommand : IRequest<UpdatedUserOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int OperationClaimId { get; set; }
        public int UserId { get; set; }

        public string[] Roles => new[] { "Admin" };

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimDto>
        {
            IUserOperationClaimRepository _userOperationClaimRepository;
            IMapper _mapper;

            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
                UpdatedUserOperationClaimDto mappedUserOperationClaimDto = _mapper.Map<UpdatedUserOperationClaimDto>(updatedUserOperationClaim);
                return mappedUserOperationClaimDto;
            }
        }
    }
}
