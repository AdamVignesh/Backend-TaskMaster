using capstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace capstone.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class AuthController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(Register model)
        {
            try
            {
                    Console.WriteLine(model.Name+"==================================================");
                    var user = new UserModel { UserName = model.Name, Email = model.Email, Role = model.Role,ImgUrl=model.ImgUrl};
                    var result = await _userManager.CreateAsync(user, model.Password);
                Console.WriteLine(user + "==================================================");

                if (result.Succeeded)
                    {
                        return Ok("Registered");
                    }
                    else
                    {
                        var errors = result.Errors.Select(e => e.Description);
                        Console.WriteLine("errors ------------------------------------------- "+errors.ToList());
                        return BadRequest(errors.ToList());
                    }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return Unauthorized(model.Email); // User not found

                }
                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (result)
                {
                    String token =  CreateToken(user);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid login attempt."+result);
                }
            }
            catch(Exception ex)
            {
                 Console.WriteLine(ex.ToString());
            }
            return Unauthorized(); // Invalid password
        }

        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials:creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                
            return jwt;
        }
   
    }
}

