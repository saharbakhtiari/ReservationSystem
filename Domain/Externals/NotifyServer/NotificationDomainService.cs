using Domain.Externals.NotifyServer.EmailNotifications;
using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer
{
    public class NotificationDomainService : INotificationDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;

        public NotificationDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
        }

        public Notification OwnerEntity { get; set; }

        public async Task<bool> CheckEmailExistance(string email,string key,CancellationToken cancellationToken)
        {
            CheckEmailNotify checkEmail = new()
            {
                Email = email,
                Key = key,
            };
            return await checkEmail.SendAsync(cancellationToken);
        }
    }
}
