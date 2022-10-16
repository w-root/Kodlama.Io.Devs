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

namespace Application.Features.ProgrammingLanguageFeature.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>
    {
        public string Name { get; set; }
        public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
        {
            public IProgrammingLanguageRepository _programmingLanguageRepository;
            public IMapper _mapper;

            public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage entityToCreate = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage createdEntity = await _programmingLanguageRepository.AddAsync(entityToCreate);
                CreatedProgrammingLanguageDto mappedEntity = _mapper.Map<CreatedProgrammingLanguageDto>(createdEntity);
                return mappedEntity;
            }
        }
    }
}
