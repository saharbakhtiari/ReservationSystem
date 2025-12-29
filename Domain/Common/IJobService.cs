using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IJobService
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
