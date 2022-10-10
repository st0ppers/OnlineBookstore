using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.BL.Interfaces;
using BookStore.Models.Models.User;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IIdentityService _identityService;
        public IdentityController(IConfiguration config, IIdentityService identityService)
        {
            _config = config;
            _identityService = identityService;
        }


        [AllowAnonymous]
        [HttpPost(nameof(Post))]
        public async Task<IActionResult> Post(LoginRequest loginRequest)
        {
            if (loginRequest != null && !string.IsNullOrEmpty(loginRequest.UserNmae) && !string.IsNullOrEmpty(loginRequest.Passowrd))
            {
                var user = await _identityService.CheckUserAndPassword(loginRequest.UserNmae, loginRequest.Passowrd);


                if (user != null)
                {
                    var userRoles = await _identityService.GetAllRoles(user);
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _config.GetSection("Jwt:Subject").Value),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.DisplayName ?? string.Empty),
                        new Claim("Username", user.UserName ?? string.Empty),
                        new Claim("Email", user.Email ?? string.Empty),
                        //new Claim("Admin","Admin")
                    };

                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                                                    expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }

            return BadRequest("Missing username and/or password");
        }


        [AllowAnonymous]
        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.UserName) || string.IsNullOrEmpty(userInfo.Password))
            {
                return BadRequest($"Username or password is missing ");
            }

            var result = await _identityService.CreateAsync(userInfo);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}
