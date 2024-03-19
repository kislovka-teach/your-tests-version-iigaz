using System.ComponentModel.DataAnnotations;

namespace Variant2.Models;

public class Comment
{
    [MaxLength(Meta.CommentMaxLength)]
    public string Text { get; set; } = null!;

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;
}