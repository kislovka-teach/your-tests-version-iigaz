namespace Variant1.Models;

public class Plant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastWatering { get; set; }

    public int ResponsibleId { get; set; }
    public User Responsible { get; set; } = null!;

    public ICollection<Display> Displays { get; } = new List<Display>();
}