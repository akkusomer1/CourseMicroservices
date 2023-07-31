using CourseMicroservices.Shared.Dtos;
using IdentityProvider.DTOs;

namespace IdentityProvider.Interfaces
{
    public interface IIdentityService
    {
        Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<ResponseDto<UserAppDto>> GetUserByNameAsync(string userName);
    }
}
