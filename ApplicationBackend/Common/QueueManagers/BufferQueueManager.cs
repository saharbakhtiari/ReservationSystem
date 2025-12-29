using Application.Common.QueueManagers;
using Domain.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Application_Backend.Rules.Commands.AddRuleVersion
{

    public class BufferQueueManager<TEntity> : IQueueManager<TEntity> where TEntity : IEntity
    {
        private readonly BufferBlock<TEntity> messages;
        public BufferQueueManager()
        {
            this.messages = new BufferBlock<TEntity>();
        }
        public async Task SendAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await this.messages.SendAsync(entity, cancellationToken);
        }
        public Task<TEntity> ReceiveAsync(CancellationToken cancellationToken)
        {
            if (messages.Count > 0)
            {
                return messages.ReceiveAsync(cancellationToken);
            }
            return default;
        }

        public int Count()
        {
            return messages.Count;
        }

    }

}
