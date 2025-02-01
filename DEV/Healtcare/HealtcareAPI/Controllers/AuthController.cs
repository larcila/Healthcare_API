using Healthcare.BR.BL;
using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Antiforgery;
using HealthCare.Data.Entity;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareAPI.Controllers
{
    /// <summary>
    /// Authorize and Autentication
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBL _UserBL;
        private readonly IRole_By_UserBL _role_By_UserBL;
        private readonly IConfiguration _configuration;

        public AuthController(IUserBL UserBL,
            IRole_By_UserBL role_By_UserBL,
            IConfiguration configuration)
        {
            _UserBL = UserBL;
            _role_By_UserBL = role_By_UserBL;
            _configuration = configuration;
        }

        /// <summary>
        /// Autentication
        /// </summary>
        /// <param name="loginDTO">Healthcare.Common.DTO.LoginDTO</param>
        /// <returns>{ Token, userDTO.User_ID, user_Rol} </returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {           
            UserDTO userDTO = await _UserBL.FindUserName(loginDTO.User_Name,needsPassword: true);


            if (userDTO == null || !_UserBL.VerifyPassword(loginDTO.Password, userDTO.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            var role_by_user = await _role_By_UserBL.GetFirstRole_By_User_By_IdUserAsync(userDTO.User_ID);
            string user_Rol = role_by_user.Role.Name;

            var token = GenerateJwtToken(userDTO.User_Name);
            return Ok(new { Token = token, Id = userDTO.User_ID, Rol = user_Rol });
        }

        /// <summary>
        ///  Get Anti-CSRF token
        /// </summary>
        /// <see cref="IConfiguration">
        /// "Jwt:Key"       : key from appsettings.json
        /// "Jwt:Issuer"    : Issuer from appsettings.json
        /// "Jwt:Audience"  : Audience from appsettings.json
        /// </see>
        /// <param name="username"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string username)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var securityKey = new SymmetricSecurityKey(key); // key from appsettings.json
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], //Issuer from appsettings.json
                audience: _configuration["Jwt:Audience"], //Audience from appsettings.json
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
