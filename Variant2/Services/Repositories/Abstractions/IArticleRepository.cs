using Variant2.Models;

namespace Variant2.Services.Repositories.Abstractions;

public interface IArticleRepository
{
    public Task<List<Article>> GetAllArticles();

    public Task<Article> AddArticle(string title, string text, User author);

    public Task<Article?> ViewArticle(int id);

    public Task<bool> DeleteArticle(int id);
}