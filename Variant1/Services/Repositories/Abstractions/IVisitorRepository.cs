using Variant1.Models;

namespace Variant1.Services.Repositories.Abstractions;

public interface IVisitorRepository
{
    public Task VisitDisplay(User user, Display display);
}