namespace Variant1.Models;

public class UserRole
{
    public int Id { get; set; }
    public string Role { get; set; } = null!;

    public ICollection<User> Users { get; } = new List<User>();
}