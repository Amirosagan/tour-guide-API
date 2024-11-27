using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.JwtAuthentication;

public class JwtTokenGenerator(JwtSettings jwtSettings) : IJwtTokenGenerator
{
    public string GenerateToken(User user , string role, bool longExpires = false)
    {
        var expireTimeHoure = 24;
        if (longExpires)
        {
            expireTimeHoure = 24 * 30;
        }
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key)
            ),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.FullName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(expireTimeHoure),
            signingCredentials: signingCredentials,
            audience: jwtSettings.Audience
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    } 
}