namespace Variant1.Dtos;

public class DisplayDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public ICollection<PlantDto> Plants { get; } = new List<PlantDto>();

    public int Visitors { get; set; }
}