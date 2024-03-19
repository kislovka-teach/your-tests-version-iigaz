using System.Security.Claims;
using Variant2.Models;

namespace Variant2.Services.Repositories.Abstractions;

public interface IUserRepository
{
    public Task<User?> LogInAsync(string login, string password);

    public Task<User?> FindByLoginAsync(string login);

    public Task<User?> FindByClaimsAsync(ClaimsPrincipal principal);
}