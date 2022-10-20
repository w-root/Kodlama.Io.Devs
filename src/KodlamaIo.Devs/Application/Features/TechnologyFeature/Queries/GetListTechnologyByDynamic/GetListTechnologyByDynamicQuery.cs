using Application.Features.TechnologyFeature.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TechnologyFeature.Queries.GetListTechnologyByDynamic
{
    public class GetListTechnologyByDynamicQuery : IRequest<TechnologyListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListTechnologyDynamicQueryHandler : IRequestHandler<GetListTechnologyByDynamicQuery, TechnologyListModel>
        {
            public ITechnologyRepository _technologyRepository;
            public IMapper _mapper;

            public GetListTechnologyDynamicQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyListModel> Handle(GetListTechnologyByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> entities = await _technologyRepository.GetListByDynamicAsync(
                                                                                    request.Dynamic,
                                                                                    include: m => m.Include(c => c.ProgrammingLanguage),
                                                                                    index: request.PageRequest.Page,
                                                                                    size: request.PageRequest.PageSize);

                TechnologyListModel mappedEntities = _mapper.Map<TechnologyListModel>(entities);
                return mappedEntities;

            }
        }

    }
}
