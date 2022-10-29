using Application.Features.TechnologyFeature.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TechnologyFeature.Queries.GetByIdTechnology
{
    public class GetByIdTechnologyQuery : IRequest<TechnologyGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, TechnologyGetByIdDto>
        {
            public ITechnologyRepository _technologyRepository;
            public IMapper _mapper;

            public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyGetByIdDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
            {
                Technology entity = await _technologyRepository.GetAsync(c => c.Id == request.Id,
                    include: c => c.Include(m => m.ProgrammingLanguage));
                TechnologyGetByIdDto mappedEntity = _mapper.Map<TechnologyGetByIdDto>(entity);
                return mappedEntity;
            }
        }
    }
}

