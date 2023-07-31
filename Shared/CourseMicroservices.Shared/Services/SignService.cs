using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CourseMicroservices.Shared.Services
{
    public class SignService
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));   
        }
    }
}
