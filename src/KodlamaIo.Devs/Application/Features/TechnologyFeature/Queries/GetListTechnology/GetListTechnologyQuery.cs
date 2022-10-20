using Application.Features.TechnologyFeature.Dtos;
using Application.Features.TechnologyFeature.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TechnologyFeature.Queries.GetListTechnologyQuery
{
    public class GetListTechnologyQuery : IRequest<TechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
        {
            public ITechnologyRepository _technologyRepository;
            public IMapper _mapper;

            public GetListTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> entities = await _technologyRepository.GetListAsync(
                                                            include: m => m.Include(c => c.ProgrammingLanguage),
                                                            index: request.PageRequest.Page,
                                                            size: request.PageRequest.PageSize
                                                            );
                TechnologyListModel mappedEntity = _mapper.Map<TechnologyListModel>(entities);
                return mappedEntity;
            }
        }
    }
}
