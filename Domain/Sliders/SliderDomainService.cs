using System.Threading;
using System.Threading.Tasks;

namespace Domain.Sliders
{
    public class SliderDomainService : ISliderDomainService
    {
        public Slider OwnerEntity { get; set; }

        public async Task SetImage(CancellationToken cancellationToken)
        {
            if (OwnerEntity.Image is null)
            {
                OwnerEntity.Image = null;
                return;
            }
            if (OwnerEntity.Image?.Id > 0)
            {
                //Do nothing 
                return;
            }
            else
            {
                await OwnerEntity.Image.DomainService.StoreFile(cancellationToken);
            }
        }
    }
}
