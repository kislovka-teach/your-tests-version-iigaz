using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Variant1.Dtos;
using Variant2.Services.Repositories.Abstractions;

namespace Variant2.Extensions.RouteGroupBuilderExtensions;

public static class MapArticlesEndpointsExtensions
{
    public static RouteGroupBuilder MapArticles(this RouteGroupBuilder group)
    {
        group.MapGet("",
            async ([FromServices] IMapper mapper, [FromServices] IArticleRepository articleRepository) =>
            mapper.Map<ArticlesInListDto>(await articleRepository.GetAllArticles()));

        return group;
    }
}