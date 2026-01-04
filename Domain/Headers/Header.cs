using Domain.Common;
using Domain.Contract.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Headers;

public class Header : AuditableEntity
{
    public string Title { get; set; }
    public string Body { get; set; }
    public byte[] DataFiles { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsDraft { get; set; }
    public int Order { get; set; }
    public string Link { get; set; }
    public IHeaderDomainService DomainService { get; set; }
    public IHeaderRepository Repository { get; set; }

    public Header()
    {
        DomainService = ServiceLocator.ServiceProvider.GetService<IHeaderDomainService>();
        Repository = ServiceLocator.ServiceProvider.GetService<IHeaderRepository>();
        DomainService.OwnerEntity = this;
        Repository.OwnerEntity = this;
    }
    public override async Task SaveAsync(CancellationToken cancellationToken)
    {
        await Repository.SaveAsync(cancellationToken);
    }

    public static async Task<Header> GetHeaderAsync(long id, CancellationToken cancellationToken)
    {
        var repository = ServiceLocator.ServiceProvider.GetService<IHeaderRepository>();
        var item = await repository.GetHeaderAsync(id, cancellationToken);
        if (item is not null)
        {
            item.Repository = repository;
            item.Repository.OwnerEntity = item;
        }
        return item;
    }

    //public static async Task<Header> GetIncludedHeaderAsync(long id, CancellationToken cancellationToken)
    //{
    //    var repository = ServiceLocator.ServiceProvider.GetService<IHeaderRepository>();
    //    var item = await repository.GetIncludedHeaderAsync(id, cancellationToken);
    //    if (item is not null)
    //    {
    //        item.Repository = repository;
    //        item.Repository.OwnerEntity = item;
    //    }
    //    return item;
    //}
}
