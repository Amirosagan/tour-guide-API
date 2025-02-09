using Domain.Entities;

namespace Application.Interfaces.UnitOfWork;

public interface ITourRepository
{
    Task AddAsync(Tour tour);
    Task<List<Tour>> GetAllAsync();
    IQueryable<Tour> GetQueryable();
    Task<Tour?> GetAsyncIncludeSessionsAndCategories(Guid id);
    void Update(Tour tour);
    void Delete(Tour tour);
}
