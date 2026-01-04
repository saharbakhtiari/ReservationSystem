//using Domain.Common;
//using Domain.Contract.Enums;
//using Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;


//namespace Domain.AdvanceSearchs
//{
//    public class FullTextAdvanceSearch : IAdvanceSearch<FullTextResultDto>
//    {
//        public async Task<PagedList<FullTextResultDto>> Search(AdvanceSearchInputDto dto, CancellationToken cancellationToken)
//        {
//            var textSearch = dto.Content.CorrectText();//  String.Join(" ", dto.Content.RemoveStopWords()); 
//            var tagIds = dto.TagIds != null && dto.TagIds.Length > 0 ? String.Join(",", dto.TagIds) : null;
//            var approvalInstitutionIds = dto.ApprovalInstitutionIds != null && dto.ApprovalInstitutionIds.Length > 0 ? String.Join(",", dto.ApprovalInstitutionIds) : null;
//            var tanghihStatusIds = dto.TanghihStatusIds != null && dto.TanghihStatusIds.Length > 0 ? String.Join(",", dto.TanghihStatusIds) : null;
//            var ruleTypeIds = dto.RuleTypeIds != null && dto.RuleTypeIds.Length > 0 ? String.Join(",", dto.RuleTypeIds) : null;

//            var objList = new List<FullTextResultDto>();
//            Dictionary<int, List<FullTextResultDto>> allResult = new Dictionary<int, List<FullTextResultDto>>();

//            switch (dto.SearchContentType)
//            {
//                case Contract.Enums.SearchContentType.None:
//                    dto.SearchContentType = SearchContentType.Title;
//                    var tempTitle = await DoSearch(dto, textSearch, tagIds, approvalInstitutionIds, tanghihStatusIds, ruleTypeIds, cancellationToken);
//                    var tempTitleMansookh = tempTitle.Where(x => x.TanghihStatusId == (long)TanghihStatusValues.Mansookh || x.TanghihStatusId == (long)TanghihStatusValues.MansookhZemni);
//                    var tempTitleNotMansookh = tempTitle.Where(x => x.TanghihStatusId != (long)TanghihStatusValues.Mansookh && x.TanghihStatusId != (long)TanghihStatusValues.MansookhZemni);
//                    objList.AddRange(tempTitleNotMansookh);
//                    objList.AddRange(tempTitleMansookh);
//                    dto.SearchContentType = SearchContentType.Context;
//                    var tempContent = await DoSearch(dto, textSearch, tagIds, approvalInstitutionIds, tanghihStatusIds, ruleTypeIds, cancellationToken);
//                    var tempContentMansookh = tempContent.Where(x => x.TanghihStatusId == (long)TanghihStatusValues.Mansookh || x.TanghihStatusId == (long)TanghihStatusValues.MansookhZemni).ToList();
//                    var tempContentNotMansookh = tempContent.Where(x => x.TanghihStatusId != (long)TanghihStatusValues.Mansookh && x.TanghihStatusId != (long)TanghihStatusValues.MansookhZemni).ToList();
//                    for (var j = 0; j < tempContentNotMansookh.Count; j++)
//                    {
//                        if (!objList.Any(e => e.Id == tempContentNotMansookh[j].Id))
//                            objList.Add(tempContentNotMansookh[j]);
//                    }
//                    for (var j = 0; j < tempContentMansookh.Count; j++)
//                    {
//                        if (!objList.Any(e => e.Id == tempContentMansookh[j].Id))
//                            objList.Add(tempContentMansookh[j]);
//                    }
//                    break;
//                case Contract.Enums.SearchContentType.Title:

//                    var tempTitle1 = await DoSearch(dto, textSearch, tagIds, approvalInstitutionIds, tanghihStatusIds, ruleTypeIds, cancellationToken);
//                    var tempTitleMansookh1 = tempTitle1.Where(x => x.TanghihStatusId == (long)TanghihStatusValues.Mansookh || x.TanghihStatusId == (long)TanghihStatusValues.MansookhZemni);
//                    var tempTitleNotMansookh1 = tempTitle1.Where(x => x.TanghihStatusId != (long)TanghihStatusValues.Mansookh && x.TanghihStatusId != (long)TanghihStatusValues.MansookhZemni);
//                    objList.AddRange(tempTitleNotMansookh1);
//                    objList.AddRange(tempTitleMansookh1);
//                    break;
//                case Contract.Enums.SearchContentType.Context:
//                    var tempContent1 = await DoSearch(dto, textSearch, tagIds, approvalInstitutionIds, tanghihStatusIds, ruleTypeIds, cancellationToken);
//                    var tempContentMansookh1 = tempContent1.Where(x => x.TanghihStatusId == (long)TanghihStatusValues.Mansookh || x.TanghihStatusId == (long)TanghihStatusValues.MansookhZemni).ToList();
//                    var tempContentNotMansookh1 = tempContent1.Where(x => x.TanghihStatusId != (long)TanghihStatusValues.Mansookh && x.TanghihStatusId != (long)TanghihStatusValues.MansookhZemni).ToList();
//                    objList.AddRange(tempContentNotMansookh1);
//                    objList.AddRange(tempContentMansookh1);

//                    break;
//            }
//            if (dto.Sort != null
//                && dto.Sort.Length > 0)
//                switch (dto.Sort[0].id)
//                {
//                    case "ApprovalInstitutionTitle":
//                        objList = dto.Sort[0].desc ? objList.OrderByDescending(e => e.ApprovalInstitution).ToList() : objList.OrderBy(e => e.ApprovalInstitution).ToList();
//                        break;
//                    case "TanghihStatusTitle":
//                        objList = dto.Sort[0].desc ? objList.OrderByDescending(e => e.TanghihStatus).ToList() : objList.OrderBy(e => e.TanghihStatus).ToList();
//                        break;
//                    case "ApprovalDate":
//                        objList = dto.Sort[0].desc ? objList.OrderByDescending(e => e.ApprovalDate).ToList() : objList.OrderBy(e => e.ApprovalDate).ToList();
//                        break;
//                    default:
//                        break;
//                }

//            var page = new List<FullTextResultDto>();
//            var uniqueRules = objList.DistinctBy(x => x.RuleId).ToList();
//            // objList= objList.OrderByDescending(x=>x.RuleId).ToList();
//            for (int i = 0; i < uniqueRules.Count(); i++)
//            {
//                page.AddRange(objList.Where(x => x.RuleId == uniqueRules[i].RuleId));

//                if (page.DistinctBy(x => x.RuleId).Count() == dto.PageSize)
//                {
//                    allResult.Add(allResult.Count, page);
//                    page = new List<FullTextResultDto>();

//                }
//            }
//            if (page.DistinctBy(x => x.RuleId).Count() > 0)
//            {
//                allResult.Add(allResult.Count, page);
//            }

//            var items =
//                allResult.ContainsKey(dto.PageNumber) ? allResult[dto.PageNumber] : allResult.Count > 0 ? allResult[0] : new List<FullTextResultDto>();

//            return new PagedList<FullTextResultDto>(items, uniqueRules.Count(), dto.PageNumber, dto.PageSize);



//        }

//        private static async Task<List<FullTextResultDto>> DoSearch(AdvanceSearchInputDto dto, string textSearch, string tagIds, string approvalInstitutionIds, string tanghihStatusIds, string ruleTypeIds, CancellationToken cancellationToken)
//        {
//            return await new RuleVersion().SprocRepository.ExecuteSearchProcedureAsync("[dbo].[USP_AdvancedSearch]", cancellationToken,
//        ("textSearch", textSearch),
//        ("SearchType", (int)dto.SearchType),
//        ("SearchContentType", (int)dto.SearchContentType),
//        ("IsPartSearch", dto.IsPartSearch),
//        ("CategoryId", dto.CategoryId),
//        ("ApprovalInstitutionIds", approvalInstitutionIds),
//        ("TanghihStatusIds", tanghihStatusIds),
//        ("RuleTypeIds", ruleTypeIds),
//        ("IsExactWord", dto.IsExactWord),
//        ("TagIds", tagIds),
//        ("ApprovalDateFrom", dto.FromApprovalDate),
//        ("ApprovalDateTo", dto.ToApprovalDate)
//        );
//        }

//    }
//}
