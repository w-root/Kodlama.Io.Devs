using Application.Features.UserOperationClaimFeature.Dtos;
using Application.Features.UserOperationClaimFeature.Rules;
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
            UserOperationClaimRules _userOperationClaimRules;

            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimRules userOperationClaimRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimRules = userOperationClaimRules;
            }

            public async Task<UpdatedUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(g => g.Id == request.Id);

                _userOperationClaimRules.UserOperationClaimShouldExistWhenRequested(userOperationClaim);

                userOperationClaim.OperationClaimId = request.OperationClaimId;
                userOperationClaim.UserId = request.UserId;

                UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
                UpdatedUserOperationClaimDto mappedUserOperationClaimDto = _mapper.Map<UpdatedUserOperationClaimDto>(updatedUserOperationClaim);
                return mappedUserOperationClaimDto;
            }
        }
    }
}
