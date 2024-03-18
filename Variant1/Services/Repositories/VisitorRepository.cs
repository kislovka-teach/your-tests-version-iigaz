using Variant1.Models;
using Variant1.Services.Repositories.Abstractions;

namespace Variant1.Services.Repositories;

public class VisitorRepository(AppDbContext context) : IVisitorRepository
{
    public async Task VisitDisplay(User user, Display display)
    {
        var visitor = new Visitor
        {
            User = user,
            Display = display,
            VisitDateTime = DateTime.UtcNow
        };
        await context.Visitors.AddAsync(visitor);
        await context.SaveChangesAsync();
    }
}