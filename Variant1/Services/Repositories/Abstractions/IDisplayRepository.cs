using Variant1.Models;

namespace Variant1.Services.Repositories.Abstractions;

public interface IDisplayRepository
{
    public Task<bool> AddPlantToDisplayAsync(int id, Plant plant);
    public Task<List<Display>> GetAllDisplaysAsync();
    public Task<Display?> AddDisplayAsync(string title, DateOnly startDate, DateOnly endDate);
    public Task<Display?> GetDisplayAsync(int id);
}