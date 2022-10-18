using Application.Features.Brands.Dtos;
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

namespace Application.Features.ProgrammingLanguageFeature.Queries.GetByIdProgrammingLanguageQuery
{
    public class GetByIdProgrammingLanguageQuery : IRequest<ProgrammingLanguageGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, ProgrammingLanguageGetByIdDto>
        {
            public IProgrammingLanguageRepository _programmingLanguageRepository;
            public IMapper _mapper;

            public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage entity = await _programmingLanguageRepository.GetAsync(c => c.Id == request.Id);
                ProgrammingLanguageGetByIdDto mappedEntity = _mapper.Map<ProgrammingLanguageGetByIdDto>(entity);
                return mappedEntity;
            }
        }
    }

}
