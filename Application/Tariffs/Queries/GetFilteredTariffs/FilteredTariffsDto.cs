using Domain.Contract.Enums;

namespace Application.Tariffs.Queries.GetFilteredTariffs
{
    public class FilteredTariffsDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
        public string Rules { get; set; }
    }
}
