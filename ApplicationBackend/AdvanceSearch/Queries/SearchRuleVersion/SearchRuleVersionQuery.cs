using Application.AdvanceSearch.Queries.SearchRuleVersion;
using AutoMapper;
using Domain.AdvanceSearchs;
using Domain.Common;
using Extensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.AdvanceSearch.Queries.SearchRuleVersion
{
    public class SearchRuleVersionQueryHandler : IRequestHandler<SearchRuleVersionQuery, List<SearchRuleVersionDto>>
    {
        private readonly IMapper _mapper;

        public SearchRuleVersionQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<SearchRuleVersionDto>> Handle(SearchRuleVersionQuery request, CancellationToken cancellationToken)
        {
            var searcher = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IAdvanceSearch<SearchRuleVersionDto>>(request.SearchType.ToString());
            var dto = _mapper.Map<AdvanceSearchInputDto>(request);
            var result = await searcher.Search(dto, cancellationToken);
            return _mapper.Map<List<SearchRuleVersionDto>>(result);
        }
    }
}
