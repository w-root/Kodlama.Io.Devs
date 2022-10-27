using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaimFeature.Dtos
{
    public class CreatedUserOperationClaimDto
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
