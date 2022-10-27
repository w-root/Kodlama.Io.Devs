using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaimFeature.Rules
{
    public class OperationClaimsRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimsRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository =  operationClaimRepository;
        }

        public async Task OperationClaimNameCanNotBeDuplicatedWhenCreated(string name)
        {
            OperationClaim result = await _operationClaimRepository.GetAsync(o => o.Name == name);
            if (result != null) throw new BusinessException("Operation claim exists !");
        }        
        
        public void OperationClaimShouldExistWhenRequested(OperationClaim operationClaim)
        {
            if (operationClaim == null) throw new BusinessException("Operation claim does not exists !");
        }
    }
}
