namespace Application.Interfaces.UnitOfWork;

public interface IUserRepository
{
    public Task ConfirmEmail(string email);
}