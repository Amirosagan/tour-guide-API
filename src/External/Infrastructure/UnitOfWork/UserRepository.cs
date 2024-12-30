using Application.Interfaces.UnitOfWork;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task ConfirmEmail(string email)
    {
        await dbContext.Database.ExecuteSqlAsync($"UPDATE AspNetUsers SET EmailConfirmed = 1 WHERE Email = '{email}'");
    }
}
