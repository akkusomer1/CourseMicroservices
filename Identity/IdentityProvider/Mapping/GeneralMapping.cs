using AutoMapper;
using IdentityProvider.DTOs;
using IdentityProvider.Models;

namespace IdentityProvider.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser, UserAppDto>().ReverseMap();
        }
    }
}
