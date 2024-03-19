namespace Variant2.Dtos;

public class RevisionDto
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public int AuthorId { get; set; }

    public int? PreviousRevisionId { get; set; }

    public int? NextRevisionId { get; set; }
}