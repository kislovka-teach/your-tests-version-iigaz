using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Variant2.Models;

public class User : IEntityTypeConfiguration<User>
{
    public int Id { get; set; }

    [MaxLength(Meta.UserLoginMaxLength)]
    public string Login { get; set; } = null!;

    [MaxLength(Meta.UserPasswordHashMaxLength)]
    public string PasswordHash { get; set; } = null!;

    public ICollection<UserRole> Roles { get; } = new List<UserRole>();

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User
        {
            Id = 1,
            Login = "ivanko",
            PasswordHash = HashPassword("Qwerty123!")
        });
    }

    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8);
        return $"{Convert.ToBase64String(salt)}.{HashPasswordWithSalt(password, salt)}";
    }

    private static string HashPasswordWithSalt(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8));
    }

    public static bool VerifyPassword(string hashed, string password)
    {
        var segments = hashed.Split('.');
        if (segments.Length != 2)
            throw new ArgumentException("Hash does not contain two parts");
        return segments[1] == HashPasswordWithSalt(password, Convert.FromBase64String(segments[0]));
    }
}