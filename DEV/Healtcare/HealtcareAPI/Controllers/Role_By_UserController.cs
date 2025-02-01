using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;
using HealthCare.Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareAPI.Controllers
{
    /// <summary>
    /// Role_By_User service
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Role_By_UserController : ControllerBase
    {
        private readonly IRole_By_UserBL _role_By_UserBL;

        public Role_By_UserController(IRole_By_UserBL role_By_UserBL)
        {
            _role_By_UserBL = role_By_UserBL;
        }


        /// <summary>
        /// Get rol by user
        /// </summary>
        /// <param name="idUser">User unique id</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Role_By_UserDTO</returns>
        // GET: api/<Role_By_UserController>
        [HttpGet("user/{idUser}")]
        public async Task<IActionResult> GetAllRol_By_User_By_IdUser(int idUser)
        {
            var rol_by_user = await _role_By_UserBL.GetAllRole_By_User_By_IdUserAsync(idUser);
            return Ok(rol_by_user);
        }

        /// <summary>
        /// Get an specific follow-up
        /// </summary>
        /// <param name="id">Unique identify of the follow-up</param>
        /// <returns>Healthcare.Common.DTO.Role_By_UserDTO</returns>
        // GET api/<Role_By_UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRol_By_UserById(int id)
        {
            var rol_by_user = await _role_By_UserBL.GetRole_By_UserByIdAsync(id);
            return Ok(rol_by_user);
        }

    }
}
