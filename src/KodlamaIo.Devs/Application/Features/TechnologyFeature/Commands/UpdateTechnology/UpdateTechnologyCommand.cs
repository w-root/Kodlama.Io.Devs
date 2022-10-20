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

namespace Application.Features.TechnologyFeature.Commands.UpdateTechnologyCommand
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            public ITechnologyRepository _technologyRepository;
            public IMapper _mapper;

            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology technology = _mapper.Map<Technology>(request);
                Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);
                UpdatedTechnologyDto mappedEntity = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);
                return mappedEntity;
            }
        }
    }
}
