using Azure.Core;
using capstone.Data;
using capstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
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
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
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
                Console.WriteLine(result + "============result=======");
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


        [HttpGet("getUserDetails")]
        public async Task<IActionResult>GetUserDetails(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(accessToken);
                Console.WriteLine("decoded token");


                IEnumerable<Claim> claims = decodedToken.Claims;
            
                string username = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                Console.WriteLine(username);

                UserModel user = await _userManager.FindByNameAsync(username);

                
                return Ok(new {userDetails = user});
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
                return BadRequest("Invaild Access Token");
        }

        [HttpGet("getUserDetailsUsingRole")]
        public async Task<IActionResult> GetUserDetailsUsingRole(string role,int project_id)
        {
            try
            {
               // var  user = await _userManager.Users.Where(user=>user.Role == role).ToListAsync();
              //  UserModel user = await _userManager.FindByNameAsync(username);

                var user =  _context.Users.Where(user => user.Role == role && ! _context.UserProjectRelation.Where(rel => rel.Projects.Project_id == project_id).Select(rel => rel.User.Id)
            .Contains(user.Id))
    .ToList();
                return Ok(new { userDetails = user });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest("Invaild Access Token");
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
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}

