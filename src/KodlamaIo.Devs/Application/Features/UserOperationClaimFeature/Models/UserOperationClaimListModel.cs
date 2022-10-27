using Application.Features.UserOperationClaimFeature.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaimFeature.Models
{
    public class UserOperationClaimListModel : BasePageableModel
    {
        public IList<GetListUserOperationClaimDto> Items { get; set; }
    }
}
