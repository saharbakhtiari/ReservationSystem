using Domain.Common;
using Domain.UnitOfWork.Uow;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Common
{
    public static class IMediatorExtention
    {
        public static async Task<TResponse> SendWithUow<TResponse>(this IMediator mediator, IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            try
            {
                var uowManager = ServiceLocator.ServiceProvider.GetService<IUnitOfWorkManager>();
                using (var uow = uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true, Timeout = TimeSpan.FromMinutes(5) }))
                {
                    var result = await mediator.Send(request, cancellationToken);
                    await uow.CompleteAsync(cancellationToken);
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
