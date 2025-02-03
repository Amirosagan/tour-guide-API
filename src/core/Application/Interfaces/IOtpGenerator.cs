using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IOtpGenerator
{
    string GenerateOtp(string token);
    string VerifyOtp(string otp);
}
