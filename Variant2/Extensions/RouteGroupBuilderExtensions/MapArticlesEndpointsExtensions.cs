using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Variant2.Dtos;
using Variant2.Services;
using Variant2.Services.Repositories.Abstractions;

namespace Variant2.Extensions.RouteGroupBuilderExtensions;

public static class MapArticlesEndpointsExtensions
{
    public static RouteGroupBuilder MapArticles(this RouteGroupBuilder group)
    {
        group.MapGet("",
            async ([FromServices] IMapper mapper,
                    [FromServices] IArticleRepository articleRepository) =>
                mapper.Map<ArticlesInListDto>(await articleRepository.GetAllArticles()));
        group.MapPost("",
            async (ClaimsPrincipal principal,
                [FromServices] IUserRepository userRepository,
                [FromServices] IArticleRepository articleRepository, [FromBody] AddArticleDto addArticleDto) =>
            {
                var user = await userRepository.FindByClaimsAsync(principal);
                if (user == null)
                    return Results.Forbid();
                var added = await articleRepository.AddArticle(addArticleDto.Title, addArticleDto.Text, user);
                return Results.Ok(added);
            }).RequireAuthorization(policy => policy.RequireRole("Editor"));
        group.MapGet("{articleId:int}",
            async (int articleId,
                [FromServices] IMapper mapper,
                [FromServices] IArticleRepository articleRepository) =>
            {
                var article = await articleRepository.ViewArticle(articleId);
                return mapper.Map<ViewArticleDto>(article);
            });
        group.MapDelete("{articleId:int}",
                async (int articleId, ClaimsPrincipal principal,
                    [FromServices] IUserRepository userRepository,
                    [FromServices] IArticleRepository articleRepository) =>
                {
                    //TODO: добавить id в claims
                    var userId = principal.FindFirstValue("userId");
                    var article = await articleRepository.ViewArticle(articleId);
                    if (article == null)
                        return Results.NotFound();
                    if (!principal.IsInRole("Moderator") && (userId == null || !int.TryParse(userId, out var uid) ||
                                                             article.AuthorId != uid))
                        return Results.Forbid();
                    await articleRepository.DeleteArticle(article);
                    return Results.NoContent();
                })
            .RequireAuthorization(policy => policy.RequireRole("Moderator", "Editor"));
        group.MapPut("{articleId:int}",
            async (int articleId, ClaimsPrincipal principal,
                [FromServices] IUserRepository userRepository,
                [FromServices] IArticleRepository articleRepository,
                [FromBody] ChangeArticleDto changeArticleDto) =>
            {
                var article = await articleRepository.ViewArticle(articleId);
                if (article == null)
                    return Results.NotFound();
                var user = await userRepository.FindByClaimsAsync(principal);
                if (user == null)
                    return Results.Forbid();
                return await articleRepository.ChangeArticle(article, changeArticleDto.Text, user)
                    ? Results.NoContent()
                    : Results.NotFound();
            }).RequireAuthorization(policy => policy.RequireRole("Editor"));
        group.MapGet("{articleId:int}/comments",
            async (int articleId,
                    [FromServices] ICommentRepository commentRepository,
                    [FromServices] IArticleRepository articleRepository,
                    [FromServices] IMapper mapper) =>
                mapper.Map<List<CommentDto>>(await commentRepository.GetArticleComments(articleId)));
        group.MapPost("{articleId:int}/comments", async (int articleId, ClaimsPrincipal principal,
            [FromServices] IUserRepository userRepository,
            [FromServices] IArticleRepository articleRepository,
            [FromServices] ICommentRepository commentRepository,
            [FromServices] IMapper mapper,
            [FromBody] AddCommentDto addCommentDto) =>
        {
            var user = await userRepository.FindByClaimsAsync(principal);
            if (user == null)
                return Results.Forbid();
            var article = await articleRepository.ViewArticle(articleId);
            if (article == null)
                return Results.NotFound();
            var comment = await commentRepository.PostComment(user, article, addCommentDto.Text);
            return Results.Ok(mapper.Map<CommentDto>(comment));
        }).RequireAuthorization(policy => policy.RequireAuthenticatedUser());

        group.MapGet("revisions/{revisionId:int}", async (int revisionId, [FromServices] IMapper mapper,
            [FromServices] IArticleRepository articleRepository,
            [FromServices] IRevisionRepository revisionRepository) =>
        {
            var revision = await revisionRepository.GetRevision(revisionId);
            return revision == null ? Results.NotFound() : Results.Ok(mapper.Map<RevisionDto>(revision));
        });
        group.MapPost("{articleId:int}/rollback", async (int articleId,
                [FromServices] IMapper mapper,
                [FromServices] IArticleRepository articleRepository,
                ClaimsPrincipal principal,
                [FromServices] IUserRepository userRepository) =>
            {
                var article = await articleRepository.ViewArticle(articleId);
                if (article == null)
                    return Results.NotFound();
                var userId = principal.FindFirstValue("userId");
                if (userId == null || !int.TryParse(userId, out var uid) || !(principal.IsInRole("Moderator") ||
                                                                              article.AuthorId == uid ||
                                                                              article.LatestRevision.AuthorId == uid))
                    return Results.Forbid();
                var rollback = await articleRepository.Rollback(article);
                return rollback == null ? Results.BadRequest() : Results.Ok(mapper.Map<ViewArticleDto>(rollback));
            })
            .RequireAuthorization(policy => policy.RequireRole("Moderator", "Editor"));


        group.MapGet("revisions/compare", async ([FromQuery] int firstRevisionId, [FromQuery] int secondRevisionId,
            [FromServices] IRevisionsComparer comparer, [FromServices] IRevisionRepository revisionRepository) =>
        {
            var revision1 = await revisionRepository.GetRevision(firstRevisionId);
            var revision2 = await revisionRepository.GetRevision(secondRevisionId);
            if (revision1 == null || revision2 == null)
                return Results.NotFound();
            return Results.Ok(comparer.Compare(revision1, revision2));
        });
        return group;
    }
}