using Application.Features.ProgrammingLanguageFeature.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageFeature.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
        {
            public IProgrammingLanguageRepository _programmingLanguageRepository;
            public IMapper _mapper;

            public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage entityToUpdate = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage updatedEntity = await _programmingLanguageRepository.UpdateAsync(entityToUpdate);
                UpdatedProgrammingLanguageDto mappedEntity = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedEntity);
                return mappedEntity;
            }
        }
    }
}
