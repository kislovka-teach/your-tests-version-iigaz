namespace Variant1.Models;

public class Visitor
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int DisplayId { get; set; }
    public Display Display { get; set; } = null!;
    
    public DateTime VisitDateTime { get; set; }
}