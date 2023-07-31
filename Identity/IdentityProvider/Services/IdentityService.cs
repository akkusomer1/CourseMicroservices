using AutoMapper;
using CourseMicroservices.Shared.Dtos;
using IdentityProvider.DTOs;
using IdentityProvider.Interfaces;
using IdentityProvider.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public IdentityService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<AppUser>(createUserDto);

            var result = await _userManager.CreateAsync(user, createUserDto.Password!);

            if (result.Succeeded)
            {
                return ResponseDto<UserAppDto>.Success(_mapper.Map<UserAppDto>(user), 200);
            }

            return ResponseDto<UserAppDto>
                .Fail(result.Errors.Select(x => x.Description).ToList(), 400);
        }

        public async Task<ResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user==null)
                ResponseDto<UserAppDto>.Fail("User Not Found", 404);
           
            
            return ResponseDto<UserAppDto>.Success(_mapper.Map<UserAppDto>(user),200);
        }
    }
}
