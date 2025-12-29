using Domain.Common;
using System.Collections.Generic;

namespace WebAppBlazor.Server.Controllers.V1
{
    public class PagedListResult<T> where T : class
    {
        public PagedListResult(PagedList<T> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
        public List<T> Items { get; set; }
        public PagedListMetaData MetaData { get; set; }
    }
}
