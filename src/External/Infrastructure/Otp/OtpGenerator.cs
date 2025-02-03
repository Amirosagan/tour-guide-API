using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Otp;

public class OtpGenerator : IOtpGenerator
{
    private readonly IMemoryCache _memoryCache;

    public OtpGenerator(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string GenerateOtp(string token)
    {
        int otp = new Random().Next(100000, 999999);
        using (var sha256 = SHA256.Create())
        {
            string otpHashed = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(otp.ToString()))
            );
            StoreOtp(otpHashed, token);
        }
        return otp.ToString();
    }

    public string VerifyOtp(string otp)
    {
        using var sha256 = SHA256.Create();
        string otpHashed = Convert.ToBase64String(
            sha256.ComputeHash(Encoding.UTF8.GetBytes(otp.ToString()))
        );
        if (_memoryCache.TryGetValue(otpHashed, out string? token))
        {
            _memoryCache.Remove(otpHashed);
            if (token != null)
            {
                return token;
            }

            return "";
        }
        return "";
    }

    private void StoreOtp(string otp, string token, int expirationInMinutes = 5)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationInMinutes),
        };
        _memoryCache.Set(otp, token, cacheEntryOptions);
    }
}
