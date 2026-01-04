using Domain.Common;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Sliders
{
    public interface ISliderDomainService : IBaseDomainService<Slider>
    {
        Task SetImage(CancellationToken cancellationToken);
    }
}
