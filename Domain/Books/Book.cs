using Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Books
{
    public class Book : AuditableEntity
    {
        public Guid ApplicationId { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

        public IBookDomainService DomainService { get; set; }
        public IBookRepository Repository { get; set; }

        public Book()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IBookDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IBookRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<Book> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IBookRepository>();
            var item = await repository.GetAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
