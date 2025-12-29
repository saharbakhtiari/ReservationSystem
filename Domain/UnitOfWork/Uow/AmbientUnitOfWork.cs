using Domain.UnitOfWork.Uow;
using System.Threading;

namespace Domain.UnitOfWork.Uow
{
    public class AmbientUnitOfWork : IAmbientUnitOfWork//, ISingletonDependency
    {
        public IUnitOfWork UnitOfWork => _currentUow.Value;

        private readonly AsyncLocal<IUnitOfWork> _currentUow;

        public AmbientUnitOfWork()
        {
            _currentUow = new AsyncLocal<IUnitOfWork>();
        }

        public void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            _currentUow.Value = unitOfWork;
        }
    }
}