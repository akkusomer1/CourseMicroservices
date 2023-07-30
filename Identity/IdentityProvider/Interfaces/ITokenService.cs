using CourseMicroservices.Shared.Dtos;
using IdentityProvider.DTOs;
using IdentityProvider.Models;

namespace IdentityProvider.Interfaces
{
    public interface ITokenService
    {
       TokenDto CreateToken(AppUser user);
    }
}
