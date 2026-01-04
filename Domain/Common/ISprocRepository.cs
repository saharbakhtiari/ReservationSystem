using Domain.AdvanceSearchs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface ISprocRepository
    {
        DbCommand GetStoredProcedure(string name, params (string, object)[] nameValueParams);

        Task<PagedList<T>> ExecuteStoredProcedureAsync<T>(string name, int pageNumber, int pageSize,  CancellationToken cancellationToken,  params (string, object)[] nameValueParams) where T : class;

        Task<PagedList<FullTextResultDto>> ExecuteSearchProcedureAsync(string name, int pageNumber, int pageSize, CancellationToken cancellationToken, params (string, object)[] nameValueParams);

        Task<List<FullTextResultDto>> ExecuteSearchProcedureAsync(string name, CancellationToken cancellationToken, params (string, object)[] nameValueParams);


        DbCommand GetStoredProcedure(string name);

    }

}
