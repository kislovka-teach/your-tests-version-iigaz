using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Variant1.Dtos;
using Variant1.Services.Repositories.Abstractions;

namespace Variant1.Extensions.RouteGroupBuilderExtensions;

public static class MapPlantsEndpointsExtensions
{
    public static RouteGroupBuilder MapPlants(this RouteGroupBuilder group)
    {
        group.MapGet("",
                async ([FromServices] IMapper mapper, [FromServices] IPlantsRepository plantsRepository) =>
                Results.Ok((object?)mapper.Map<List<PlantDto>>(await plantsRepository.GetAllPlantsAsync())))
            .RequireAuthorization(builder => builder.RequireRole("Gardener", "Manager"));
        group.MapPost("",
            async (ClaimsPrincipal principal, [FromServices] IMapper mapper,
                [FromServices] IUserRepository userRepository,
                [FromServices] IPlantsRepository plantsRepository, [FromBody] AddPlantDto addPlantDto) =>
            {
                var login = principal.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
                if (login == null)
                    return Results.Forbid();
                var user = await userRepository.FindByLoginAsync(login);
                if (user == null)
                    return Results.Forbid();
                var plant = await plantsRepository.AddPlantAsync(addPlantDto.Name, user);
                return Results.Ok(mapper.Map<PlantDto>(plant));
            }).RequireAuthorization(builder => builder.RequireRole("Gardener"));
        group.MapPost("/{id:int}/water", async ([FromRoute] int id,
            [FromServices] IPlantsRepository plantsRepository) => await plantsRepository.WaterPlantAsync(id)
            ? Results.NoContent()
            : Results.NotFound()).RequireAuthorization(builder => builder.RequireRole("Gardener"));

        return group;
    }
}