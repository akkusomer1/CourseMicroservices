using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityProvider.Services
{
    public class SignService
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));   
        }
    }
}
