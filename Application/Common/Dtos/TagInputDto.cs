namespace Application.Common.Dtos
{
    public class TagInputDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool IsApproved { get; set; } = false!;
    }
}