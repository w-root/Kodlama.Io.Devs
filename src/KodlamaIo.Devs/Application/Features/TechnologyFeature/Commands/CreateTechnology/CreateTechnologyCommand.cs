using Application.Features.TechnologyFeature.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TechnologyFeature.Commands.CreateTechnologyCommand
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            public ITechnologyRepository _technologyRepository;
            public IMapper _mapper;

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology technology = _mapper.Map<Technology>(request);
                Technology createdTechnology = await _technologyRepository.AddAsync(technology);
                CreatedTechnologyDto mappedEntity = _mapper.Map<CreatedTechnologyDto>(createdTechnology);
                return mappedEntity;
            }
        }
    }
}
