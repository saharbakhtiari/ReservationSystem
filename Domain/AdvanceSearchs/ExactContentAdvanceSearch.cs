//using Domain.Common;
//using Domain.RuleCategories;
//using Domain.RuleVersions;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Domain.AdvanceSearchs
//{
//    public class ExactContentAdvanceSearch : IAdvanceSearch<RuleVersion>
//    {
//        public async Task<PagedList<RuleVersion>> Search(AdvanceSearchInputDto dto, CancellationToken cancellationToken)
//        {
//            var categories = new List<RuleCategory>();
//            if (dto.CategoryId.HasValue)
//            {
//                categories = await new RuleCategory().DomainService.GetRecursiveChildsAsync(dto.CategoryId.Value, cancellationToken);
//            }
//            var categoryIds = categories?.Select(c => c.Id).ToList();
//            return await new RuleVersion().Repository.ExactContentSearchAsync(dto.Content.Trim(),
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
//                                                                               dto.PageNumber, dto.PageSize,
//                                                                            cancellationToken);

            
//        }
//    }
//}
