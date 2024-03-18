namespace Variant1.Models;

public class Review
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;
    
    public int Rating { get; set; }
}