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

namespace Application.Features.TechnologyFeature.Commands.DeleteTechnologyCommand
{
    public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
        {
            public ITechnologyRepository _technologyRepository;
            public IMapper _mapper;

            public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology technology = _mapper.Map<Technology>(request);
                Technology deletedTechnology = await _technologyRepository.DeleteAsync(technology);
                DeletedTechnologyDto mappedEntity = _mapper.Map<DeletedTechnologyDto>(deletedTechnology);
                return mappedEntity;
            }
        }
    }
}
