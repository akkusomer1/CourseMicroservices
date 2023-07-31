using CourseMicroservices.Shared.ControllerBases;
using IdentityProvider.DTOs;
using IdentityProvider.Interfaces;
using IdentityProvider.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IIdentityService _identityService;

        public UsersController(IAuthenticationService authenticationService, IIdentityService identityService)
        {
            _authenticationService = authenticationService;
            _identityService = identityService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateToken(LoginDto login)
        {
            return CreateActionResult(await _authenticationService.CreateTokenAsync(login));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken)
        {
            return CreateActionResult(await _authenticationService.CreateTokenByRefreshTokenAsync(refreshToken));
        }

        [Authorize]
        [HttpDelete("[action]/{refreshToken}")]
        public async Task<IActionResult>RevokeRefreshToken(string refreshToken)
        {
            return CreateActionResult(await _authenticationService.RevokeRefreshToken(refreshToken));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return CreateActionResult(await _identityService.CreateUserAsync(createUserDto));
        }

        [Authorize]
        [HttpGet("[action]/{userName}")]
        public async Task<IActionResult> GetUser(string userName)
        {
            return CreateActionResult(await _identityService.GetUserByNameAsync(userName));
        }

    }
}
