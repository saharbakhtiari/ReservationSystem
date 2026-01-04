using Domain.Common;
using Domain.Contract.Enums;
using Domain.SliderFiles;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Sliders;

public class Slider : AuditableEntity
{
    public string Title { get; set; }
    public string Link { get; set; }
    public string Body { get; set; }
    public SliderFile Image { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPublished { get; set; }
    public int Order { get; set; }
    public SliderType Type { get; set; }
    public ISliderDomainService DomainService { get; set; }
    public ISliderRepository Repository { get; set; }

    public Slider()
    {
        DomainService = ServiceLocator.ServiceProvider.GetService<ISliderDomainService>();
        Repository = ServiceLocator.ServiceProvider.GetService<ISliderRepository>();
        DomainService.OwnerEntity = this;
        Repository.OwnerEntity = this;
    }
    public override async Task SaveAsync(CancellationToken cancellationToken)
    {
        await Repository.SaveAsync(cancellationToken);
    }

    public static async Task<Slider> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = ServiceLocator.ServiceProvider.GetService<ISliderRepository>();
        var item = await repository.GetAsync(id, cancellationToken);
        if (item is not null)
        {
            item.Repository = repository;
            item.Repository.OwnerEntity = item;
        }
        return item;
    }

    //public static async Task<Slider> GetIncludedSliderAsync(long id, CancellationToken cancellationToken)
    //{
    //    var repository = ServiceLocator.ServiceProvider.GetService<ISliderRepository>();
    //    var item = await repository.GetIncludedSliderAsync(id, cancellationToken);
    //    if (item is not null)
    //    {
    //        item.Repository = repository;
    //        item.Repository.OwnerEntity = item;
    //    }
    //    return item;
    //}
}
