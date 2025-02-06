namespace Application.Interfaces.UnitOfWork;

public interface IUnitOfWorkRepo
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
