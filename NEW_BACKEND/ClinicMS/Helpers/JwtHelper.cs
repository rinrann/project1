using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ClinicMS.Helpers
{
    public static class JwtHelper
    {
        private static string SecretKey  => ConfigurationManager.AppSettings["Jwt:SecretKey"];
        private static string Issuer     => ConfigurationManager.AppSettings["Jwt:Issuer"];
        private static string Audience   => ConfigurationManager.AppSettings["Jwt:Audience"];
        private static int    ExpiryMins => int.Parse(ConfigurationManager.AppSettings["Jwt:AccessTokenExpiryMinutes"] ?? "480");

        public static string GenerateToken(
            string userId, string userName, string roleId, string roleName,
            string compCode, string yearCode)
        {
            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name,           userName),
                new Claim("roleId",                  roleId   ?? ""),
                new Claim(ClaimTypes.Role,           roleName ?? ""),
                new Claim("compCode",                compCode ?? ""),
                new Claim("yearCode",                yearCode ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                          new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                          ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer:             Issuer,
                audience:           Audience,
                claims:             claims,
                notBefore:          DateTime.UtcNow,
                expires:            DateTime.UtcNow.AddMinutes(ExpiryMins),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal ValidateToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var handler = new JwtSecurityTokenHandler();

            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = key,
                ValidateIssuer           = true,
                ValidIssuer              = Issuer,
                ValidateAudience         = true,
                ValidAudience            = Audience,
                ValidateLifetime         = true,
                ClockSkew                = TimeSpan.Zero
            };

            return handler.ValidateToken(token, parameters, out _);
        }
    }
}
