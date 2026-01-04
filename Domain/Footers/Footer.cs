using Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Footers;

public class FooterLink
{
    public string Title { get; set; }
    public string Url { get; set; }
}
public class Footer : AuditableEntity
{
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsDeleted { get; set; }
    public int Order { get; set; }
    public bool IsDraft { get; set; }
    public List<FooterLink> Links { get; set; }
    public IFooterDomainService DomainService { get; set; }
    public IFooterRepository Repository { get; set; }

    public Footer()
    {
        DomainService = ServiceLocator.ServiceProvider.GetService<IFooterDomainService>();
        Repository = ServiceLocator.ServiceProvider.GetService<IFooterRepository>();
        DomainService.OwnerEntity = this;
        Repository.OwnerEntity = this;
    }
    public override async Task SaveAsync(CancellationToken cancellationToken)
    {
        await Repository.SaveAsync(cancellationToken);
    }

    public static async Task<Footer> GetFooterAsync(long id, CancellationToken cancellationToken)
    {
        var repository = ServiceLocator.ServiceProvider.GetService<IFooterRepository>();
        var item = await repository.GetFooterAsync(id, cancellationToken);
        if (item is not null)
        {
            item.Repository = repository;
            item.Repository.OwnerEntity = item;
        }
        return item;
    }

    //public static async Task<Footer> GetIncludedFooterAsync(long id, CancellationToken cancellationToken)
    //{
    //    var repository = ServiceLocator.ServiceProvider.GetService<IFooterRepository>();
    //    var item = await repository.GetIncludedFooterAsync(id, cancellationToken);
    //    if (item is not null)
    //    {
    //        item.Repository = repository;
    //        item.Repository.OwnerEntity = item;
    //    }
    //    return item;
    //}
}
