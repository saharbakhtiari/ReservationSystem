using Domain.UnitOfWork;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IGenericApiCallerRepository<TEntity>
        where TEntity : class, IEntity
    {
        TEntity OwnerEntity { get; set; }
        Task<TResponse> CallService<TResponse>(string url);
    }
}
