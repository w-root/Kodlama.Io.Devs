using Application.Features.TechnologyFeature.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageFeature.Models
{
    public class ProgrammingLanguageListModel : BasePageableModel
    {
        public IList<TechnologyListDto> Items { get; set; }
    }
}
