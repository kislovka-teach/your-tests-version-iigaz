namespace Variant2.Dtos;

public class ViewArticleDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public RevisionDto LatestRevision { get; set; } = null!;
}