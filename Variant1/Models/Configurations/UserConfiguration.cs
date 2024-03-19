using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Variant1.Models.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.FirstName).HasMaxLength(Meta.UserFirstNameMaxLength);
        builder.Property(user => user.LastName).HasMaxLength(Meta.UserLastNameMaxLength);
        builder.Property(user => user.Login).HasMaxLength(Meta.UserLoginMaxLength);
        builder.Property(user => user.PasswordHash).HasMaxLength(512);
        builder.HasAlternateKey(user => user.Login);

        builder.HasData(new User
        {
            Id = 1,
            FirstName = "Ivan",
            LastName = "Ivanovich",
            Login = "ivanko",
            PasswordHash = User.HashPassword("Qwerty123!")
        });
    }
}