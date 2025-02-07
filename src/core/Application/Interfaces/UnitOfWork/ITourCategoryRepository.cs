using Domain.Entities;

namespace Application.Interfaces.UnitOfWork;

public interface ITourCategoryRepository
{
    Task<List<TourCategory>> GetAllAsync();
    Task<TourCategory?> GetAsync(int id);
    Task<TourCategory?> GetAsyncWithTracking(int id);
    void Add(TourCategory tourCategory);
    void Delete(TourCategory tourCategory);
}
