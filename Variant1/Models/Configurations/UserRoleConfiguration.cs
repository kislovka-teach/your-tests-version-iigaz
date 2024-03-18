using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Variant1.Models.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.Property(role => role.Role).HasMaxLength(16);

        builder.HasData(new UserRole
            {
                Id = 1,
                Role = "User"
            },
            new UserRole
            {
                Id = 2,
                Role = "Visitor"
            }, new UserRole
            {
                Id = 3,
                Role = "Gardener"
            }, new UserRole
            {
                Id = 4,
                Role = "Manager"
            });
    }
}