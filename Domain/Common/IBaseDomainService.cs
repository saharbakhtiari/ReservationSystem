using System.Threading;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IBaseDomainService<TEntity>
    {
        TEntity OwnerEntity { get; set; }
    }
}