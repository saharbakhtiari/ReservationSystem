//using Application.AdvanceSearch.Queries.SearchRuleVersion;
//using AutoMapper;
//using Domain.AdvanceSearchs;
//using Domain.Common;
//using Extensions;
//using MediatR;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Application_Backend.AdvanceSearch.Queries.SearchRuleVersion
//{
//    public class FullTextSearchRuleVersionQueryHandler : IRequestHandler<FullTextSearchRuleVersionQuery, PagedList<SearchRuleVersionDto>>
//    {
//        private readonly IMapper _mapper;

//        public FullTextSearchRuleVersionQueryHandler(IMapper mapper)
//        {
//            _mapper = mapper;
//        }

//        public async Task<PagedList<SearchRuleVersionDto>> Handle(FullTextSearchRuleVersionQuery request, CancellationToken cancellationToken)
//        {
//            var searcher = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IAdvanceSearch<FullTextResultDto>>(typeof(FullTextAdvanceSearch).Name);
//            var dto = _mapper.Map<AdvanceSearchInputDto>(request);
//            if(!string.IsNullOrEmpty(dto.Content))
//            {
//                dto.Content = dto.Content.PersianNormalize().Trim();
//                var filter = string.Empty;
//                if (dto.Content.Contains("\""))
//                {
//                    dto.IsExactWord = false;
//                    var exactwords = System.Text.RegularExpressions.Regex.Matches(dto.Content, "\"(.+?)\"").Cast<Match>()
//                                        .Select(s => s.Groups[1].Value).ToArray();
//                    foreach (var exactword in exactwords)
//                    {
//                        filter += exactword.Replace("\"", "").Trim() + '#';
//                        dto.Content = dto.Content.Replace(exactword, "").Replace("\"", "");
//                    }

//                    var words = dto.Content.Split(" ");
//                    foreach (var word in words)
//                    {
//                        if (word.Length > 1)
//                            filter += word + '#';
//                    }



//                }
//                else
//                {
//                    var words = dto.Content.Split(" ");
//                    foreach (var word in words)
//                    {
//                        if (word.Length > 1)
//                            filter += word + '#';
//                    }

//                }
//                dto.Content = filter.TrimEnd('#');
//            }
           

//            var result = await searcher.Search(dto, cancellationToken);

//            var map = _mapper.Map<PagedList<SearchRuleVersionDto>>(result);
//            map.MetaData = result.MetaData;
//            return map;
//        }


//    }
//}
