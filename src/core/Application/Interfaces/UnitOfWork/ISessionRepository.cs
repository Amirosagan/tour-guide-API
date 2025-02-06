using Domain.Entities;

namespace Application.Interfaces.UnitOfWork;

public interface ISessionRepository
{
    Task<List<Session>> GetAllWithTourIdAsync(Guid id);
    void Add(Session session);
    void Delete(Session session);
    void Update(Session session);
}
