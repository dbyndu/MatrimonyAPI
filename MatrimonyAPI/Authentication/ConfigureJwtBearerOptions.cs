using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MatrimonyAPI.Authentication
{
    public class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly IOptions<JwtAuthentication> _jwtAuthentication;

        public ConfigureJwtBearerOptions(IOptions<JwtAuthentication> jwtAuthentication)
        {
            _jwtAuthentication = jwtAuthentication ?? throw new System.ArgumentNullException(nameof(jwtAuthentication));
        }
        public void PostConfigure(string name, JwtBearerOptions options)
        {
            var jwtAuthentication = _jwtAuthentication.Value;
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            //options.ClaimsIssuer = jwtAuthentication.ValidIssuer;
            options.IncludeErrorDetails = true;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                //ValidIssuer = jwtAuthentication.ValidIssuer,
                //ValidAudience = jwtAuthentication.ValidAudience,
                //IssuerSigningKey = jwtAuthentication.SymmetricSecurityKey,
                ClockSkew = TimeSpan.Zero,
                NameClaimType = ClaimTypes.NameIdentifier
            };
        }
    }
}

                
                
                
                
                