using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Variant1.Dtos;
using Variant1.Services.Repositories.Abstractions;

namespace Variant1.Extensions.RouteGroupBuilderExtensions;

public static class MapDisplaysEndpointsExtensions
{
    public static RouteGroupBuilder MapDisplays(this RouteGroupBuilder group)
    {
        group.MapGet("",
            async ([FromServices] IMapper mapper, [FromServices] IDisplayRepository displayRepository) =>
            mapper.Map<List<DisplayDto>>(await displayRepository.GetAllDisplaysAsync()));
        group.MapPost("",
            async ([FromServices] IMapper mapper, [FromServices] IDisplayRepository displayRepository,
                [FromBody] AddDisplayDto addDisplayDto) =>
            {
                var display = await displayRepository.AddDisplayAsync(addDisplayDto.Title,
                    new DateOnly(addDisplayDto.StartDateYear, addDisplayDto.StartDateMonth, addDisplayDto.StartDateDay),
                    new DateOnly(addDisplayDto.EndDateYear, addDisplayDto.EndDateMonth, addDisplayDto.EndDateDay));
                return display == null ? Results.BadRequest() : Results.Ok(mapper.Map<DisplayDto>(display));
            }).RequireAuthorization(builder => builder.RequireRole("Manager"));
        group.MapGet("{id:int}",
            async ([FromRoute] int id, [FromServices] IMapper mapper,
                [FromServices] IDisplayRepository displayRepository) =>
            {
                var display = await displayRepository.GetDisplayAsync(id);
                return display == null ? Results.NotFound() : Results.Ok(mapper.Map<DisplayDto>(display));
            }).RequireAuthorization(builder => builder.RequireRole("Visitor", "Manager"));
        group.MapPost("{id:int}/visit",
            async ([FromRoute] int id, ClaimsPrincipal principal, [FromServices] IMapper mapper,
                [FromServices] IDisplayRepository displayRepository, [FromServices] IUserRepository userRepository,
                [FromServices] IVisitorRepository visitorRepository) =>
            {
                var display = await displayRepository.GetDisplayAsync(id);
                if (display == null)
                    return Results.NotFound();
                var login = principal.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
                if (login == null)
                    return Results.Forbid();
                var user = await userRepository.FindByLoginAsync(login);
                if (user == null)
                    return Results.Forbid();
                await visitorRepository.VisitDisplay(user, display);
                return Results.NoContent();
            }).RequireAuthorization(builder => builder.RequireRole("Visitor"));

        return group;
    }
}