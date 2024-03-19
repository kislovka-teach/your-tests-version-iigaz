using Microsoft.EntityFrameworkCore;
using Variant2.Models;
using Variant2.Services.Repositories.Abstractions;

namespace Variant2.Services.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository
{
    public async Task<List<Comment>> GetArticleComments(int articleId)
    {
        return await context.Comments.Where(comment => comment.ArticleId == articleId).ToListAsync();
    }

    public async Task<Comment> PostComment(User user, Article article, string text)
    {
        var comment = new Comment
        {
            Author = user,
            Article = article,
            Text = text
        };
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return comment;
    }
}