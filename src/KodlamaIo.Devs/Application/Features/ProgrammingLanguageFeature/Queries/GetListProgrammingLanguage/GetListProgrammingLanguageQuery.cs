using Application.Features.ProgrammingLanguageFeature.Dtos;
using Application.Features.ProgrammingLanguageFeature.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageFeature.Queries.GetListProgrammingLanguage
{
    public class GetListProgrammingLanguage : IRequest<ProgrammingLanguageListModel>
    {
        public PageRequest pageRequest { get; set; }
        public class GetListProgrammingLanguageQueryHandler : IRequestHandler<GetListProgrammingLanguage, ProgrammingLanguageListModel>
        {
            public IProgrammingLanguageRepository _programmingLanguageRepository;
            public IMapper _mapper;

            public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguage request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingLanguage> entities = await _programmingLanguageRepository.GetListAsync(index: request.pageRequest.Page, size: request.pageRequest.PageSize);
                ProgrammingLanguageListModel mappedEntities = _mapper.Map<ProgrammingLanguageListModel>(entities);
                return mappedEntities;

            }
        }
    }

}
