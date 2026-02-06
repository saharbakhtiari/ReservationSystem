using Domain.Contract.Enums;

namespace Application.Tariffs.Queries.GetTariff
{
    public class GetTariffByIdDto
    {
        public long Id { get; set; }
        public GetTariffByIdSpaceDto Space { get; set; } = null!;
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
        public string Rules { get; set; }
    }
    public class GetTariffByIdSpaceDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public int Capacity { get; set; }
        public string Location { get; set; }
        public SpaceType Type { get; set; }
    }

}
