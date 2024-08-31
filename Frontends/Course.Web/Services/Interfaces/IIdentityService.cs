using Course.Web.Models;
using CourseMicroservices.Shared.Dtos;

namespace Course.Web.Services.Interfaces
{
    public interface IIdentityService
    {

        Task<ResponseDto<bool>> SignIn(SigninInput signinInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
