using Variant2.Models;

namespace Variant2.Services;

public interface IRevisionsComparer
{
    public Task<string> Compare(Revision revision1, Revision revision2);
}