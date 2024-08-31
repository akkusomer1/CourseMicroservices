using Course.Web.Models;
using Course.Web.Services.Interfaces;
using CourseMicroservices.Shared.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Course.Web.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<ResponseDto<bool>> SignIn(SigninInput signinInput)
        {

            string signin_info = JsonSerializer.Serialize(signinInput);
            StringContent content = new StringContent(signin_info, Encoding.UTF8, MediaTypeNames.Application.Json);
            HttpResponseMessage response = await _httpClient.PostAsync($"{_serviceApiSettings.IdentityBaseUri}/api/Users/CreateToken", content);

       
            string content_response = await response.Content.ReadAsStringAsync();



             ApiResponse<TokenResponse> tokenResponse = JsonSerializer.Deserialize<ApiResponse<TokenResponse>>(content_response,new JsonSerializerOptions()
             {
                 PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
             });


            if (!response.IsSuccessStatusCode)
            {
                return ResponseDto<bool>.Fail(tokenResponse.Errors,400);
            }



            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            JwtSecurityToken accessToken = handler.ReadJwtToken(tokenResponse.Data.AccessToken);


            if (accessToken != null)
            {
                List<Claim> claims = accessToken.Claims.ToList();


                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                AuthenticationProperties authenticationProperties = new AuthenticationProperties();

                // DateTime'ı Unix zaman damgasına (epoch time) dönüştürme
                var expirationDateTime = tokenResponse.Data.AccessTokenExpiration;
                var epochTime = ((DateTimeOffset)expirationDateTime).ToUnixTimeSeconds();

                // Epoch time'ı Double'a dönüştürme
                var expires_at = Convert.ToDouble(epochTime);
                
                authenticationProperties.StoreTokens(
                    new List<AuthenticationToken>
                    {

                        new AuthenticationToken(){Name =  "access_token",Value = tokenResponse.Data.AccessToken},
                        new AuthenticationToken(){Name =  "refresh_token",Value = tokenResponse.Data.RefreshToken},

                        //Kültür bilgisine bağlı olmadan .ToString() ile yazdır önemli!!
                       
                        new AuthenticationToken { Name = "expires_at", Value = DateTime.UtcNow.AddSeconds(expires_at).ToString("o") }

                    });

                authenticationProperties.IsPersistent = signinInput.IsRemember;


                await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                return ResponseDto<bool>.Success(true, 200);
            }
            throw new NotImplementedException();
        }

        public Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task RevokeRefreshToken()
        {
            throw new NotImplementedException();
        }


    }
}
