using jwt.Dto;
using jwt.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

namespace jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _user;
        private readonly jwtConfig _jwt;
        public AuthenticationController(UserManager<IdentityUser> user,IOptions<jwtConfig> jwt)//i was use IOptions<jwtConfig> jwt to solve probleam of (resolve service)
        {
            _user = user; ;
            _jwt = jwt.Value;
            
        }

        [HttpPost("Registration")]

        public async Task<IActionResult> Registration([FromBody]UserRegister userRegister)
        {
            if (ModelState.IsValid)
            {
                var exit = await _user.FindByEmailAsync(userRegister.email);
                if (exit != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        result = false,
                        listError=new List<string>
                        {
                            "the email is exit , user other "
                        }
                    });
                }

                var new_user  =new  IdentityUser(){
                    UserName = userRegister.username,
                    Email = userRegister.email,
                };

                var result =await _user.CreateAsync(new_user,userRegister.password);

                if(result.Succeeded)
                {
                    //Genrate JWT

                    var tokene = GenrateJWTtoken(new_user);
                    return Ok(new AuthResult()
                    {
                        result = true,
                         token = tokene
                    });

                }
                return BadRequest(new AuthResult()
                {
                    result = false,
                    listError = new List<string>
                        {
                            "Something have error , return agin"
                        }
                });


            }
 
            return BadRequest();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]Userlogin userlogin)
        {
            if(ModelState.IsValid)
            {
                var user_exit = await _user.FindByEmailAsync(userlogin.Email);
                if (user_exit == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        result = false,
                        listError = new List<string>
                        {
                            "invalid payload"
                        }
                    });
                }

                var correctPassword = await _user.CheckPasswordAsync(user_exit, userlogin.password);
                if (correctPassword)
                {
                    var tokene = GenrateJWTtoken(user_exit);
                    return Ok(new AuthResult()
                    {
                        result = true,
                        token = tokene
                    });
                }
                return BadRequest(new AuthResult()
                {
                    result = false,
                    listError = new List<string>
                        {
                            "invalid payload"
                        }
                });

            }

            return BadRequest(new AuthResult()
            {
                result = false,
                listError = new List<string>
                        {
                            "invalid payload"
                        }
            });
        }


      
        
        
        private string GenrateJWTtoken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_jwt.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),

                }),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);

        }
    
    }
}
