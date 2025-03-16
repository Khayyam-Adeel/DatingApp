using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection addIdentityservices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var keyBytes = Encoding.ASCII.GetBytes(config["TokenKey"]);
                    // Ensure key is at least 32 bytes (256 bits)
                    if (keyBytes.Length < 32)
                    {
                        var paddedKey = new byte[32];
                        Array.Copy(keyBytes, paddedKey, Math.Min(keyBytes.Length, 32));
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(paddedKey),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidIssuer = "daterica-api",
                            ValidAudience = "daterica-client",
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                    }
                    else
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidIssuer = "daterica-api",
                            ValidAudience = "daterica-client",
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                    }
                });
            return services;
        }
    }
}