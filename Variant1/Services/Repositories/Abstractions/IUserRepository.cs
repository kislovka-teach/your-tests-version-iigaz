using Variant1.Models;

namespace Variant1.Services.Repositories.Abstractions;

public interface IUserRepository
{
    public Task<User?> LogInAsync(string login, string password);

    public Task<User?> FindByLoginAsync(string login);
}