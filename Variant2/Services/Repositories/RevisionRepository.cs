using Microsoft.EntityFrameworkCore;
using Variant2.Models;
using Variant2.Services.Repositories.Abstractions;

namespace Variant2.Services.Repositories;

public class RevisionRepository(AppDbContext context) : IRevisionRepository
{
    public async Task<Revision> AddInitialRevision(string text, User author)
    {
        var revision = new Revision
        {
            Author = author,
            DateTime = DateTime.UtcNow,
            Text = text
        };
        var added = await context.Revisions.AddAsync(revision);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Revision?> AddNextRevision(Revision current, string text, User author)
    {
        var revision = new Revision
        {
            Author = author,
            DateTime = DateTime.UtcNow,
            Text = text,
            PreviousRevision = current
        };
        var added = await context.Revisions.AddAsync(revision);
        current.NextRevision = added.Entity;
        await context.SaveChangesAsync();
        return added.Entity;
    }


    public async Task<string> RevisionsToText(Revision latest)
    {
        return latest.Text;
    }

    public async Task<Revision?> GetRevision(int id)
    {
        return await context.Revisions.SingleOrDefaultAsync(revision => revision.Id == id);
    }

    public async Task<Revision?> Rollback(Revision latest)
    {
        if (latest.PreviousRevisionId == null)
            return null;
        var previous = await GetRevision(latest.PreviousRevisionId.Value);
        if (previous == null)
            return null;
        previous.NextRevision = null;
        previous.NextRevisionId = null;
        await context.SaveChangesAsync();
        return previous;
    }
}