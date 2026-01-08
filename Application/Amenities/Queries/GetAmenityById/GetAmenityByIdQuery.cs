using MediatR;

namespace Application.Amenitys.Queries.GetAmenity
{
    public class GetAmenityByIdQuery : IRequest<GetAmenityByIdDto>
    {
        public long Id { get; set; }
    }
}
