using MediatR;

namespace Application.Tariffs.Queries.GetTariff
{
    public class GetTariffByIdQuery : IRequest<GetTariffByIdDto>
    {
        public long Id { get; set; }
    }
}
