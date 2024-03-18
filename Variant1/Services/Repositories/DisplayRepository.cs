using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Variant1.Models;
using Variant1.Services.Repositories.Abstractions;

namespace Variant1.Services.Repositories;

public class DisplayRepository(AppDbContext context, IValidator<Display> validator) : IDisplayRepository
{
    public async Task<bool> AddPlantToDisplayAsync(int id, Plant plant)
    {
        var display = await GetDisplayAsync(id);
        if (display == null)
            return false;
        display.Plants.Add(plant);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Display>> GetAllDisplaysAsync()
    {
        return await context.Displays.Include(display => display.Visitors).Include(display => display.Plants)
            .ToListAsync();
    }

    public async Task<Display?> AddDisplayAsync(string title, DateOnly startDate, DateOnly endDate)
    {
        var display = new Display
        {
            Title = title,
            StartDate = startDate,
            EndDate = endDate
        };
        var validated = await validator.ValidateAsync(display);
        if (!validated.IsValid)
            return null;
        var added = await context.Displays.AddAsync(display);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Display?> GetDisplayAsync(int id)
    {
        return await context.Displays.Include(display => display.Visitors).Include(display => display.Plants)
            .SingleOrDefaultAsync(display => display.Id == id);
    }
}