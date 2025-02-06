using Domain.Enums;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Seeding.identity;

public static class SeedAdmin
{
    public static async Task SeedAsync(UserManager<NormalUser> userManager)
    {
        var admin = new NormalUser
        {
            FirstName = "Admin",
            LastName = "Admin",
            PhoneNumber = "01091584875",
            Email = "loco.Admin@loco.com",
            UserName = "SuperAdmin",
        };

        await userManager.CreateAsync(admin, "Admin@1234ForTesting");
        await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
        await userManager.AddToRoleAsync(admin, Roles.NormalUser.ToString());

        var token = await userManager.GenerateEmailConfirmationTokenAsync(admin);

        await userManager.ConfirmEmailAsync(admin, token);
    }
}
