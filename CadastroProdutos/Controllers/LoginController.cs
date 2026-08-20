using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CadastroProdutos.Models;

namespace CadastroProdutos.Controllers

{
   [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        
        private IConfiguration configuration;
        
        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public ActionResult Login(Login login)

        {

            string role;
            
            if (login.Usuario == "Admin" && login.Senha == "1234" )
            {
                role = "Admin";
            }
            else if (login.Usuario == "Cliente" && login.Senha == "1234")
            {
                role = "Cliente";
            }

            else
            {
                return Unauthorized();
            }

            var jwtConfig = configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtConfig["Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();
          var tokenDescriptor = new SecurityTokenDescriptor()
{
    Subject = new ClaimsIdentity(new[]
    {
      
        new Claim(ClaimTypes.Name, login.Usuario), 
        
        
        new Claim(ClaimTypes.Role, role) 
        
       
    }),
    Expires = DateTime.UtcNow.AddHours(1),
    Issuer = jwtConfig["Issuer"],
    Audience = jwtConfig["Audience"],
    SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha256Signature
    )
};

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {Token = tokenString});
        } 


    }   


}