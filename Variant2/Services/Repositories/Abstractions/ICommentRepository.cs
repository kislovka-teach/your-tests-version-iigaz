using Variant2.Models;

namespace Variant2.Services.Repositories.Abstractions;

public interface ICommentRepository
{
    public Task<List<Comment>> GetArticleComments(int articleId);
    public Task<Comment> PostComment(User user, string text);
}