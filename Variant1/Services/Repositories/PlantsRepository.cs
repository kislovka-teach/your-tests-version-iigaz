using Microsoft.EntityFrameworkCore;
using Variant1.Models;
using Variant1.Services.Repositories.Abstractions;

namespace Variant1.Services.Repositories;

public class PlantsRepository(AppDbContext context) : IPlantsRepository
{
    public async Task<List<Plant>> GetAllPlantsAsync()
    {
        return await context.Plants.ToListAsync();
    }

    public async Task<Plant> AddPlantAsync(string name, User responsible)
    {
        var plant = new Plant
        {
            Name = name,
            Responsible = responsible,
            LastWatering = DateTime.UtcNow
        };
        var addedPlant = await context.Plants.AddAsync(plant);
        await context.SaveChangesAsync();
        return addedPlant.Entity;
    }

    public async Task<Plant?> GetPlantAsync(int id)
    {
        return await context.Plants.SingleOrDefaultAsync(plant => plant.Id == id);
    }

    public async Task<bool> DeletePlantAsync(int id)
    {
        var plant = await GetPlantAsync(id);
        if (plant == null)
            return false;
        context.Plants.Remove(plant);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> WaterPlantAsync(int id)
    {
        var plant = await GetPlantAsync(id);
        if (plant == null)
            return false;
        plant.LastWatering = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return true;
    }
}