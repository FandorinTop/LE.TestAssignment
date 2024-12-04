using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LE.Services.Options
{
    public static class TokenFactory
    {
        public static JwtSecurityToken GenerateToken(IEnumerable<Claim> authClaims, JwtOption jwtOption)
        {
            var secret = jwtOption.Secret;
            var issuer = jwtOption.ValidIssuer;
            var audience = jwtOption.ValidAudience;
            var expirationTime = DateTime.Now.AddHours(jwtOption.TokenValidityInHours);

            return GenerateToken(authClaims, secret, issuer, audience, expirationTime);
        }

        private static JwtSecurityToken GenerateToken(IEnumerable<Claim> authClaims, string secret, string issuer, string audience, DateTime expirationTime)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var token = new JwtSecurityToken(
                        issuer: issuer,
                        audience: audience,
                        expires: expirationTime,
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
