using System.Text;

using Application.Interfaces;
using Application.Interfaces.UnitOfWork;

using Domain.Identity;

using Infrastructure.Data;
using Infrastructure.EmailService;
using Infrastructure.Google;
using Infrastructure.JwtAuthentication;
using Infrastructure.Otp;
using Infrastructure.Storage;
using Infrastructure.UnitOfWork;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = new JwtSettings();
        var oAuthGoogleSettings = new OAuthGoogleSettings();
        var emailSettings = new EmailSettings();
        var dropboxSettings = new DropboxSettings();
            
        configuration.Bind("OAuthSettings:Google", oAuthGoogleSettings);
        configuration.Bind(nameof(JwtSettings), jwtSettings);
        configuration.Bind(nameof(EmailSettings), emailSettings);
        configuration.Bind(nameof(DropboxSettings), dropboxSettings);
        services.AddSingleton(jwtSettings);
        services.AddSingleton(oAuthGoogleSettings);
        services.AddSingleton(emailSettings);
        services.AddSingleton(dropboxSettings);

        services.AddMemoryCache();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IEmailServiceSender, EmailServiceSender>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IOtpGenerator, OtpGenerator>();

        services.AddScoped<IUserRepository, UserRepository>();

        services
            .AddIdentityCore<NormalUser>(o =>
            {
                o.User.RequireUniqueEmail = true;
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequiredLength = 8;
                o.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddIdentityCore<TourGuide>(o =>
            {
                o.User.RequireUniqueEmail = true;
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequiredLength = 8;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services
            .AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                cfg.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key)
                    ),
                }
            )
            .AddCookie(IdentityConstants.ExternalScheme)
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.ClientId = oAuthGoogleSettings.ClientId;
                options.ClientSecret = oAuthGoogleSettings.ClientSecret;
                options.CallbackPath = oAuthGoogleSettings.RedirectUri;
                oAuthGoogleSettings.Scopes.ForEach(options.Scope.Add);
                options.AccessType = "offline";
                options.SaveTokens = true;
            });
        services.AddAuthorization();

        return services;
    }
}