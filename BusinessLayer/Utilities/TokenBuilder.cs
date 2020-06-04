using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessLayer.Utilities
{
    public static class TokenBuilder
    {
        internal static TokenValidationParameters tokenValidationParams;

        public static void ConfigureJwtAuthentication(this IServiceCollection services)
        {
            tokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                RequireSignedTokens = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.FromMinutes(60)
            };
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParams;

                options.IncludeErrorDetails = true;

                options.RequireHttpsMetadata = false;

            });
        }
        public static JwtSecurityToken CreateJsonWebToken(
               string username,
               IEnumerable<string> roles,
               string audienceUri,
               string issuerUri,
               Guid applicationId,
               DateTime expires,
               List<Claim> claims,
               string deviceId = null,
               bool isReAuthToken = false)
        {
            //var claims = new List<Claim>();
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            var head = new JwtHeader();
            var payload = new JwtPayload(claims.ToArray());
            var jwt = new JwtSecurityToken(issuerUri, audienceUri, claims, DateTime.UtcNow, expires);
            return jwt;
        }


        public static JwtSecurityToken CreateJsonWebTokens(
       string username,
       IEnumerable<string> roles,
       string audienceUri,
       string issuerUri,
       Guid applicationId,
       DateTime expires,
       List<Claim> claims,
       SigningCredentials credentials,
       string deviceId = null,
       bool isReAuthToken = false)
        {
            //var claims = new List<Claim>();
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            var head = new JwtHeader();
            var payload = new JwtPayload(claims.ToArray());
            var jwt = new JwtSecurityToken(issuerUri, audienceUri, claims, DateTime.UtcNow, expires, credentials);
            return jwt;
        }
    }
}
