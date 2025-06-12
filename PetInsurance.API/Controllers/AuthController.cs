using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetInsurance.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetInsurance.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // In a real application, use a user store or Identity system
        private const string ValidUsername = "test";
        private const string ValidPassword = "pass";

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT if successful.
        /// </summary>
        /// <param name="creds">User credentials</param>
        [HttpPost("auth/login")]
        public IActionResult Login([FromBody] Credentials creds)
        {
            if (creds == null || string.IsNullOrWhiteSpace(creds.Username) || string.IsNullOrWhiteSpace(creds.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            if (creds.Username == ValidUsername && creds.Password == ValidPassword)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, creds.Username)
                };

                var secretKey = _configuration["Jwt:Secret"];
                if (string.IsNullOrEmpty(secretKey))
                {
                    return StatusCode(500, new { message = "JWT secret is not configured." });
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = tokenString });
            }

            return Unauthorized(new { message = "Invalid credentials." });
        }
    }
}