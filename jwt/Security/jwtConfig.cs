using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt.Security
{
    public class jwtConfig
    {
        public string SecretKey { get; set; }
         
    }



}
