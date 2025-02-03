using Domain.Identity;

namespace Application.Interfaces;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user, string role, bool longExpires = false);
}
