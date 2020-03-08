using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonyAPI.Authentication
{
    public class AuthenticationHelper
    {
        public string GenerateToken(JwtAuthentication jwtAuthentication,string userId,string email)
        {
            string returnValue = null;

            //var key = jwtAuthentication.SecurityKey;
            //var issuer = jwtAuthentication.ValidIssuer;
            //var audience = jwtAuthentication.ValidAudience;
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.UniqueName,userId),
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer : jwtAuthentication.ValidIssuer,
                audience:jwtAuthentication.ValidAudience,
                claims:claims,
                expires :DateTime.UtcNow.AddMinutes(int.Parse(jwtAuthentication.Expires)),
                notBefore: DateTime.UtcNow,
                signingCredentials: jwtAuthentication.SigningCredentials                
                );

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            
            //var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow.AddMinutes(10),
            //    Issuer = issuer,
            //    Audience = audience,
            //    SigningCredentials = signingCredentials
            //};
            returnValue = new JwtSecurityTokenHandler().WriteToken(token);

            return returnValue;
        }
        private void ValidateToken(IConfiguration configuration, IServiceCollection services)
        {
            var audianceConfig = configuration.GetSection("KeyDetails");
            var key = audianceConfig["key"];
            var issuer = audianceConfig["Issuer"];
            var audience = audianceConfig["Audience"];

            var keyByteArray = Encoding.ASCII.GetBytes(key);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                option.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}
