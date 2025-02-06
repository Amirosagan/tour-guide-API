using Application.Interfaces.UnitOfWork;
using Infrastructure.Data;

namespace Infrastructure.UnitOfWork;

public class UnitOfWorkRepo : IUnitOfWorkRepo
{
    private readonly ApplicationDbContext _context;

    public UnitOfWorkRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken: cancellationToken);
    }
}
