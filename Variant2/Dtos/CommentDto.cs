namespace Variant2.Dtos;

public class CommentDto
{
    public string Text { get; set; } = null!;

    public int AuthorId { get; set; }

    public int ArticleId { get; set; }
}