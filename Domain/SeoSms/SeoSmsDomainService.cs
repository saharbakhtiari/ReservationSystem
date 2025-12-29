using Domain.UnitOfWork.Uow;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SeoSms
{
    public class SeoSmsDomainService : ISeoSmsDomainService
    {
        public SeoSms OwnerEntity { get; set; }
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SeoSmsDomainService(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task DeleteOlds(CancellationToken cancellationToken)
        {
            var olds = await OwnerEntity.Repository.GetOldSmsesAsync(cancellationToken);
            olds.ForEach(async a =>
            {
                using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false }, requiresNew: true))
                {
                    a.IsDeleted = true;
                    await a.SaveAsync(cancellationToken);
                    await uow.CompleteAsync(cancellationToken);
                }
            });
        }
    }
}
