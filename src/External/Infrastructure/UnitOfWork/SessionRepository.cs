using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork;

public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Session>> GetAllWithTourIdAsync(Guid id)
    {
        return _context.Sessions.AsNoTracking().Where(x => x.TourId == id).ToListAsync();
    }

    public void Add(Session session)
    {
        _context.Sessions.Add(session);
    }

    public void Delete(Session session)
    {
        _context.Sessions.Remove(session);
    }

    public void Update(Session session)
    {
        _context.Sessions.Update(session);
    }
}
