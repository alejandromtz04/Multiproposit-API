using ApiLogin.Infraestructure.DB;
using ApiLogin.Infraestructure.Security;
using ApiLogin.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiLogin.Extensions;

public static class ExtensionMethods
{
    // Pasamos IConfiguration para poder leer el appsettings.json para el JWT
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        // 1. INYECCIÓN DE DEPENDENCIAS (Servicios)
        services.AddScoped<IAuthenticationService, LdapAuthentication>();
        services.AddSingleton<ConexionService>();
        services.AddSingleton<IJWTManager, JWTManager>();

        // 2. CONFIGURACIÓN JWT
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var Key = Encoding.UTF8.GetBytes(config["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JWT:Issuer"],
                ValidAudience = config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero,
            };
            o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        // 3. CONFIGURACIÓN CORS
        services.AddCors(options =>
        {
            options.AddPolicy(name: "_CEPAPolicy",
                policy =>
                {
                    policy.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
        });

        return services;
    }
}