using Variant1.Models;

namespace Variant1.Services.Repositories.Abstractions;

public interface IPlantsRepository
{
    public Task<List<Plant>> GetAllPlantsAsync();
    public Task<Plant> AddPlantAsync(string name, User responsible);
    public Task<Plant?> GetPlantAsync(int id);
    public Task<bool> DeletePlantAsync(int id);
    public Task<bool> WaterPlantAsync(int id);
}