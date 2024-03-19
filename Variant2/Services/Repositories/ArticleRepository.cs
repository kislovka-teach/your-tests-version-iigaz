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

    public async Task DeleteArticle(Article article)
    {
        context.Articles.Remove(article);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ChangeArticle(Article article, string text, User author)
    {
        var revision = await revisionRepository.AddNextRevision(article.LatestRevision, text, author);
        if (revision == null)
            return false;
        article.LatestRevision = revision;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Article?> Rollback(Article article)
    {
        var revision = await revisionRepository.Rollback(article.LatestRevision);
        if (revision == null)
            return null;
        article.LatestRevision = revision;
        await context.SaveChangesAsync();
        return article;
    }
}