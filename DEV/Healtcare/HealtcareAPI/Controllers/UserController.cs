using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareAPI.Controllers
{
    /// <summary>
    /// User service
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly IRole_By_UserBL _role_By_UserBL;

        public UserController(IUserBL userBL, IRole_By_UserBL role_By_UserBL)
        {
            _userBL = userBL;
            _role_By_UserBL = role_By_UserBL;
        }

        /// <summary>
        /// List all User
        /// </summary>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var Users = await _userBL.GetAllUserAsync();
            return Ok(Users);
        }

        /// <summary>
        /// Get the User by filtering by id
        /// </summary>
        /// <param name="id">is the unique ID in the table</param>
        /// <returns>Healthcare.Common.DTO.UserDTO</returns>
        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var User = await _userBL.GetUserByIdAsync(id);
            if (User == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            return Ok(User);
        }

        /// <summary>
        /// Add new User in database
        /// </summary>
        /// <param name="userDTO">Healthcare.Common.DTO.UserDTO</param>
        /// <returns>NoContent()</returns>
        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUpdateUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO UserDto = new() {
                Birth_Date = userDTO.Birth_Date,
                Email = userDTO.Email,
                Family_Name = userDTO.Family_Name,
                First_Name = userDTO.First_Name,
                Identification = userDTO.Identification,
                User_Name = userDTO.User_Name,
                Registration_Date = userDTO.Registration_Date,
                Modify_Date = userDTO.Modify_Date,
                Registration_User_ID = userDTO.Registration_User_ID,
                Update_User_ID = userDTO.Update_User_ID,
                Password = userDTO.Password
            };

            
            await _userBL.AddUserAsync(UserDto);

            var users = await _userBL.GetAllUserAsync();
            int idUser = users.FirstOrDefault(item => item.User_Name == UserDto.User_Name
                && item.Identification == UserDto.Identification).User_ID;


            Role_By_UserDTO role_By_UserDTO = new()
            {
                Role_By_User_Id = userDTO.Role_By_Users.Role_By_User_Id,
                Role_ID = userDTO.Role_By_Users.Role_ID,
                User_ID = idUser
            };

            await _role_By_UserBL.AddRole_By_UserAsync(role_By_UserDTO);

            return CreatedAtAction(nameof(GetUserById), new { id = UserDto.User_ID }, UserDto);
        }

        /// <summary>
        /// Modify or edit User
        /// </summary>
        /// <param name="userDTO">Healthcare.Common.DTO.UserDTO</param>
        /// <returns>NoContent()</returns>
        // PUT: api/User/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] AddUpdateUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO UserDto = new()
            {
                User_ID = userDTO.User_ID,
                Birth_Date = userDTO.Birth_Date,
                Email = userDTO.Email,
                Family_Name = userDTO.Family_Name,
                First_Name = userDTO.First_Name,
                Identification = userDTO.Identification,
                User_Name = userDTO.User_Name,
                Registration_Date = userDTO.Registration_Date,
                Modify_Date = userDTO.Modify_Date,
                Registration_User_ID = userDTO.Registration_User_ID,
                Update_User_ID = userDTO.Update_User_ID,
            };

            Role_By_UserDTO role_By_UserDTO = new()
            {
                Role_By_User_Id = userDTO.Role_By_Users.Role_By_User_Id,
                Role_ID = userDTO.Role_By_Users.Role_ID,
                User_ID = userDTO.Role_By_Users.User_ID,
            };

            var existingUser = await _userBL.GetUserByIdAsync(UserDto.User_ID);
            if (existingUser == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            await _userBL.UpdateUserAsync(UserDto);
            await _role_By_UserBL.UpdateRole_By_UserAsync(role_By_UserDTO);


            return NoContent();
        }

        /// <summary>
        /// Delete an existing User
        /// </summary>
        /// <param name="id">User_ID</param>
        /// <returns>NoContent()</returns>
        // DELETE: api/User/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _userBL.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            await _userBL.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
