using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork;

public class TourRepository : ITourRepository
{
    private readonly ApplicationDbContext _context;

    public TourRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Tour tour)
    {
        await _context.Tours.AddAsync(tour);
    }

    public Task<List<Tour>> GetAllAsync()
    {
        return _context.Tours.AsNoTracking().ToListAsync();
    }

    public IQueryable<Tour> GetQueryable()
    {
        return _context.Tours.AsNoTracking();
    }

    public Task<Tour?> GetAsync(Guid id)
    {
        return _context.Tours.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(Tour tour)
    {
        _context.Tours.Update(tour);
    }

    public void Delete(Tour tour)
    {
        _context.Tours.Remove(tour);
    }
}
