using IdentityProvider.DTOs;
using IdentityProvider.Interfaces;
using IdentityProvider.Models;
using IdentityProvider.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace IdentityProvider.Services
{
    public class TokenService : ITokenService
    {
        private readonly ICustomTokenOptions _tokenOptions;

        public TokenService(ICustomTokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        private string CreateRefreshToken()
        {
            Byte[] numberByte = new byte[35];
            var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }


        public TokenDto CreateToken(AppUser user)
        {
            var accessTokenExpiration = DateTime.UtcNow.ToLocalTime().AddDays(_tokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.UtcNow.ToLocalTime().AddDays(_tokenOptions.RefreshTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                claims: GetClaims(user, _tokenOptions.Audiences),
                notBefore: DateTime.UtcNow.ToLocalTime(),
                expires: accessTokenExpiration,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

           var accessToken= handler.WriteToken(jwtSecurityToken);

            return new TokenDto()
            {
                AccessToken = accessToken,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken=CreateRefreshToken(),
                RefreshTokenExpiration=refreshTokenExpiration
            };
        }

        private IEnumerable<Claim> GetClaims(AppUser user, string[] audience)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };
            claims.AddRange(audience.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return claims;
        }
    }
}
