using System.ComponentModel.DataAnnotations;

namespace Variant2.Models;

public class Article
{
    public int Id { get; set; }

    [MaxLength(Meta.ArticleTitleMaxLength)]
    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public int LatestRevisionId { get; set; }
    public Revision LatestRevision { get; set; } = null!;
}