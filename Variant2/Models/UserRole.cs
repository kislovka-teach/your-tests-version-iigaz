using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Variant2.Models;

public class UserRole : IEntityTypeConfiguration<UserRole>
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [MaxLength(16)]
    public string Role { get; set; } = null!;

    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(new UserRole
        {
            UserId = 1,
            Role = "Editor"
        });
    }
}