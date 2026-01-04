//using Domain.RuleCategories;
//using Domain.RuleVersions;
//using Extensions;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Threading;
//using System.Linq;
//using Domain.Common;

//namespace Domain.AdvanceSearchs
//{
//    public class ExceptWordsAdvanceSearch : IAdvanceSearch<RuleVersion>
//    {
//        public async Task<PagedList<RuleVersion>> Search(AdvanceSearchInputDto dto, CancellationToken cancellationToken)
//        {
//            var searchWords = dto.Content?.ToSeperateInput(" "); ;
//            var categories = new List<RuleCategory>();
//            if (dto.CategoryId.HasValue)
//            {
//                categories = await new RuleCategory().DomainService.GetRecursiveChildsAsync(dto.CategoryId.Value, cancellationToken);
//            }
//            var categoryIds = categories?.Select(c => c.Id).ToList();
//           return await new RuleVersion().Repository.ExceptWordSearchAsync(searchWords,
//                                                                            dto.SearchContentType,
//                                                                            categoryIds,
//                                                                            dto.ApprovalInstitutionIds[0],
//                                                                            dto.TanghihStatusIds[0],
//                                                                            dto.RuleTypeIds[0],
//                                                                            dto.TagIds,
//                                                                            dto.IsPartSearch,
//                                                                            dto.IsExactWord,
//                                                                            dto.FromApprovalDate,
//                                                                            dto.ToApprovalDate,
//                                                                            dto.PageNumber, dto.PageSize,
//                                                                            cancellationToken);

           
//        }
//    }
//}
