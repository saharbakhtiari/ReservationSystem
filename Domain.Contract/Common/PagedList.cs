using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public class PagedList<T> : List<T>
    {
        public PagedListMetaData MetaData { get; set; }
        public object SummaryData { get; set; }
        public PagedList()
        {
                
        }
        public PagedList(List<T> items, PagedListMetaData metaData)
        {
            MetaData = metaData;
            AddRange(items);
        }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new PagedListMetaData
            {
                PageSize = pageSize,
                TotalCount = count,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }
    }
}
