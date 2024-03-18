namespace Variant1.Models;

public class Display
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public ICollection<Plant> Plants { get; } = new List<Plant>();

    public ICollection<Review> Reviews { get; } = new List<Review>();

    public ICollection<Visitor> Visitors { get; } = new List<Visitor>();
}