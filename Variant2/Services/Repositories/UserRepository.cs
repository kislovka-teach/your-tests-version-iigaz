using Microsoft.EntityFrameworkCore;
using Variant2.Models;
using Variant2.Services.Repositories.Abstractions;

namespace Variant2.Services.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> LogInAsync(string login, string password)
    {
        var user = await FindByLoginAsync(login);
        if (user == null || !User.VerifyPassword(user.PasswordHash, password))
            return null;
        return user;
    }

    public async Task<User?> FindByLoginAsync(string login)
    {
        return await context.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Login == login);
    }
}