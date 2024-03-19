using Microsoft.EntityFrameworkCore;
using Variant1.Models;
using Variant1.Services.Repositories.Abstractions;

namespace Variant1.Services.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> LogInAsync(string login, string password)
    {
        var user = await FindByLoginAsync(login);
        if (user == null || !User.VerifyPassword(user.PasswordHash, password))
            return null;
        if (user.Roles.Count == 0)
        {
            user.Roles.Add(await context.UserRoles.SingleAsync(role => role.Role == "User"));
            await context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<User?> FindByLoginAsync(string login)
    {
        return await context.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Login == login);
    }
}