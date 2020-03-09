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
        public string GenerateToken(JwtAuthentication jwtAuthentication, string userId, string email, string role)
        {
            return this.GenerateToken(jwtAuthentication, this.GetClaims(userId, email, role));
        }
        private string GenerateToken(JwtAuthentication jwtAuthentication, IEnumerable<Claim> allClaims)
        {
            string returnValue = null;
            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthentication.Key));
            var creds = new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256);
            var claims = allClaims;
            var token = new JwtSecurityToken(
                issuer: jwtAuthentication.Issuer,
                audience: jwtAuthentication.Issuer,
                claims: claims,
                //expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtAuthentication.Expires)),
                expires: DateTime.UtcNow.AddMinutes(10),
                notBefore: DateTime.UtcNow,
                signingCredentials: creds
                ) ;

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

        public IEnumerable<Claim> GetClaims(string userId, string email, string role)
        {
            List<Claim> allClaims = new List<Claim>();
            allClaims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, userId));
            allClaims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
            allClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            allClaims.Add(new Claim(ClaimTypes.Role, role));
            return allClaims;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(JwtAuthentication jwtAuthentication,string token)
        {
            if (!token.Contains("Bearer"))
            {
                return null;
            }
            var tokeValue = token.Split(new[] { ' ' }, 2);
            if(tokeValue!=null && tokeValue.Length > 0)
            {
                var actualToken = tokeValue[1];
                var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthentication.Key));
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = singingKey,
                    ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(actualToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
                return principal;
            }
            return null;
        }

        public string ValidateToken(JwtAuthentication jwtAuthentication,string token)
        {
            var principle = GetPrincipalFromExpiredToken(jwtAuthentication, token);
            if(principle!=null && principle.Identity != null)
            {
                var newJwtToken = this.GenerateToken(jwtAuthentication, principle.Claims);
                return newJwtToken;

            }
            return null;
        }

        //private void ValidateToken(IConfiguration configuration, IServiceCollection services)
        //{
        //    var audianceConfig = configuration.GetSection("KeyDetails");
        //    var key = audianceConfig["key"];
        //    var issuer = audianceConfig["Issuer"];
        //    var audience = audianceConfig["Audience"];

        //    var keyByteArray = Encoding.ASCII.GetBytes(key);
        //    var signingKey = new SymmetricSecurityKey(keyByteArray);

        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = signingKey,
        //        ValidateIssuer = true,
        //        ValidIssuer = issuer,
        //        ValidateAudience = true,
        //        ValidAudience = audience,
        //        ValidateLifetime = true,
        //        ClockSkew = TimeSpan.Zero
        //    };

        //    services.AddAuthentication(options => {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(option => {
        //        option.TokenValidationParameters = tokenValidationParameters;
        //    });
        //}
    }
}
