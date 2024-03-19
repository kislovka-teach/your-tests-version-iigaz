using Variant2.Models;
using Variant2.Services.Repositories.Abstractions;

namespace Variant2.Services;

public class RevisionsComparer(IRevisionRepository revisionRepository) : IRevisionsComparer
{
    public async Task<string> Compare(Revision revision1, Revision revision2)
    {
        var t1 = await revisionRepository.RevisionsToText(revision1);
        var t2 = await revisionRepository.RevisionsToText(revision2);
        var lines1 = t1.Split('\n').Select(l => '-' + l).ToArray();
        var lines2 = t2.Split('\n').Select(l => '+' + l).ToArray();

        return $"@@ -1,{lines1.Length} +1,{lines2.Length} @@\n" + string.Join('\n', lines1) +
               string.Join('\n', lines2) + "\n";
    }
}