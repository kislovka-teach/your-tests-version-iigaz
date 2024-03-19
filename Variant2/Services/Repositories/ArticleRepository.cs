using Microsoft.EntityFrameworkCore;
using Variant2.Models;
using Variant2.Services.Repositories.Abstractions;

namespace Variant2.Services.Repositories;

public class ArticleRepository(AppDbContext context, IRevisionRepository revisionRepository) : IArticleRepository
{
    public async Task<List<Article>> GetAllArticles()
    {
        return await context.Articles.ToListAsync();
    }

    public async Task<Article> AddArticle(string title, string text, User author)
    {
        var revision = await revisionRepository.AddInitialRevision(text, author);
        var article = new Article
        {
            Title = title,
            Author = author,
            LatestRevision = revision
        };
        var added = await context.Articles.AddAsync(article);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Article?> ViewArticle(int id)
    {
        return await context.Articles.Include(article => article.LatestRevision)
            .SingleOrDefaultAsync(article => article.Id == id);
    }

    public async Task<bool> DeleteArticle(int id)
    {
        var article = await ViewArticle(id);
        if (article == null)
            return false;
        context.Articles.Remove(article);
        await context.SaveChangesAsync();
        return true;
    }
}