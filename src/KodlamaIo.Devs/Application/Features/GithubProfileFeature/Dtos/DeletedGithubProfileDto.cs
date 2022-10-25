using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfileFeature.Dtos
{
    public class DeletedGithubProfileDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Github { get; set; }
    }
}
