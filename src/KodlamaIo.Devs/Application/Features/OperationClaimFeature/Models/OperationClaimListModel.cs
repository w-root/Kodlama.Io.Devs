using Application.Features.OperationClaimFeature.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaimFeature.Models
{
    public class OperationClaimListModel : BasePageableModel
    {
        public List<GetListOperationClaimDto> Items { get; set; }
    }
}
