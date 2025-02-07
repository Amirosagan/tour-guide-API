using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork;

public class TourCategoryRepository : ITourCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public TourCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<TourCategory>> GetAllAsync()
    {
        return _context.TourCategories.AsNoTracking().ToListAsync();
    }

    public Task<TourCategory?> GetAsync(int id)
    {
        return _context.TourCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<TourCategory?> GetAsyncWithTracking(int id)
    {
        return _context.TourCategories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(TourCategory tourCategory)
    {
        _context.TourCategories.Add(tourCategory);
    }

    public void Delete(TourCategory tourCategory)
    {
        _context.TourCategories.Remove(tourCategory);
    }
}
