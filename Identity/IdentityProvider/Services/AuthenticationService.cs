using CourseMicroservices.Shared.Dtos;
using IdentityProvider.DTOs;
using IdentityProvider.Interfaces;
using IdentityProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IdentityAppDbContext _identityContext;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IdentityAppDbContext identityContext, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;
            _tokenService = tokenService;
        }

        public async Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
                return ResponseDto<TokenDto>.Fail("User Not Found", 404);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null
                || !await _userManager.CheckPasswordAsync(user, loginDto.Password)
                )
                return ResponseDto<TokenDto>.Fail("Email or password is wrong", 404);

            var token = _tokenService.CreateToken(user);


            var refreshToken = await _identityContext.RefreshTokens.SingleOrDefaultAsync(x => x.UserId == user.Id);
            if (refreshToken is null)
            {
                await _identityContext.RefreshTokens.AddAsync(
                       new RefreshToken
                       {
                           Content = token.RefreshToken,
                           Expiration = token.RefreshTokenExpiration,
                           UserId = user.Id
                       });
            }
            else
            {
                refreshToken.Content = token.RefreshToken;
                refreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _identityContext.SaveChangesAsync();

            return ResponseDto<TokenDto>.Success(token, 200);
        }

        public async Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string content)
        {
            var refreshToken = await _identityContext.RefreshTokens.SingleOrDefaultAsync(x => x.Content == content);

            if (refreshToken == null)
                return ResponseDto<TokenDto>.Fail("Refresh Token Not Found", 404);

            var user = await _userManager.FindByIdAsync(refreshToken.UserId);
            if (user == null)
                return ResponseDto<TokenDto>.Fail("User Token Not Found", 404);

            var token = _tokenService.CreateToken(user);

            refreshToken.Content = token.RefreshToken;
            refreshToken.Expiration = token.RefreshTokenExpiration;

            await _identityContext.SaveChangesAsync();

            return ResponseDto<TokenDto>.Success(token, 200);
        }

        public async Task<ResponseDto<NoContentDto>> RevokeRefreshToken(string content)
        {
            var existRefreshToken = await _identityContext.RefreshTokens.SingleOrDefaultAsync(x => x.Content ==content);

            if (existRefreshToken == null)
            {
                return ResponseDto<NoContentDto>.Fail("Refresh Token not found", 404);
            }

            _identityContext.RefreshTokens.Remove(existRefreshToken);
            await _identityContext.SaveChangesAsync();
            return ResponseDto<NoContentDto>.Success(200);
        }
    }
}
