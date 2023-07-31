using CourseMicroservices.Shared.Dtos;
using IdentityProvider.DTOs;

namespace IdentityProvider.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<ResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken);
    }
}
