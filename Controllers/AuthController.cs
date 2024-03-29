using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Services.Interfaces;


namespace PSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ICustomersService _service;
        private readonly IConfiguration _config;

        public AuthController(ICustomersService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] CustomerLogin body)
        {
            var currentUser = _service.Authenticate(body);

            if(currentUser == null) 
            {
                var unauthorizedResponseJson = new { message = "Invalid username or password" };
                return Unauthorized(unauthorizedResponseJson);
            }

            var responseJson = new { token = GenerateToken(currentUser) };
            return Ok(responseJson);
        }

        private string GenerateToken(Customer customer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Role, customer.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
