using Variant2.Models;

namespace Variant2.Services.Repositories.Abstractions;

public interface IRevisionRepository
{
    public Task<Revision> AddInitialRevision(string text, User author);

    public Task<Revision?> AddNextRevision(Revision current, string text, User author);

    public Task<string> RevisionsToText(Revision latest);

    public Task<Revision?> GetRevision(int id);

    public Task<Revision?> Rollback(Revision latest);
}