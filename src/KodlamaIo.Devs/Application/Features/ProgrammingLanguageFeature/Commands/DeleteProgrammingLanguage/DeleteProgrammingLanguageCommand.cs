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

namespace Application.Features.ProgrammingLanguageFeature.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
        {
            public IProgrammingLanguageRepository _programmingLanguageRepository;
            public IMapper _mapper;

            public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage entityToDelete = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage deletedEntity = await _programmingLanguageRepository.DeleteAsync(entityToDelete);
                DeletedProgrammingLanguageDto mappedEntity = _mapper.Map<DeletedProgrammingLanguageDto>(deletedEntity);
                return mappedEntity;
            }
        }

    }
}
