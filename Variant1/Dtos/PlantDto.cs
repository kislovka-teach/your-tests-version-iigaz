namespace Variant1.Dtos;

public class PlantDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastWatering { get; set; }

    public int ResponsibleId { get; set; }
}