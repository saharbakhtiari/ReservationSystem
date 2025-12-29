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

        Task<IList<T>> ExecuteStoredProcedureAsync<T>(string name, CancellationToken cancellationToken,  params (string, object)[] nameValueParams) where T : class;

        DbCommand GetStoredProcedure(string name);

    }

}
