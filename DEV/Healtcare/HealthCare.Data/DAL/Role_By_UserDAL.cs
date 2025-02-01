using Healthcare.Common.Action;
using Healthcare.Common.DTO;
using HealthCare.Data.Context;
using HealthCare.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Data.DAL
{
    /// <summary>
    /// Table data Access Layer:  [dbo].[Roles_By_Users]]
    /// </summary>
    public class Role_By_UserDAL : IRole_By_UserRepository
    {
        
        private readonly HealthcareDbcontext _context;
        public Role_By_UserDAL(HealthcareDbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds information to the table: [dbo].[Roles_By_Users]
        /// </summary>
        /// <param name="role_By_UserDTO">Healthcare.Common.DTO.Role_By_UserDTO</param>
        /// <returns>void</returns>
        public async Task AddRole_By_UserAsync(Role_By_UserDTO role_By_UserDTO)
        {
            Role_By_User Role_By_User = MapRole_By_UserDTO_To_Role_By_User(role_By_UserDTO);

            await _context.AddAsync(Role_By_User);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a row from table: [dbo].[Roles_By_Users]
        /// </summary>
        /// <param name="id">unique identity of the Roles_By_Users table (Column name: Role_By_User_Id)</param>
        /// <returns>void</returns>
        public async Task DeleteRole_By_UserAsync(int id)
        {
            var Role_By_User = await _context.Roles_By_Users.FindAsync(id);
            if (Role_By_User != null)
            {
                _context.Roles_By_Users.Remove(Role_By_User);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all Roles_By_Users from [dbo].[Roles_By_Users] table by unique Id
        /// </summary>
        /// <param name="user_ID">user id</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Role_By_UserDTO</returns>
        public async Task<IEnumerable<Role_By_UserDTO>> GetAllRole_By_User_By_IdUserAsync(int user_ID)
        {
            var Role_By_Users = await _context.Roles_By_Users.Where(x => x.User_ID == user_ID).ToListAsync();
            return MapRole_By_User_To_Role_By_UserDTO_List(Role_By_Users);
        }

        /// <summary>
        /// Get all Roles_By_Users from [dbo].[Roles_By_Users] table by User_Id
        /// </summary>
        /// <param name="id">unique identity of the Roles_By_Users table (Column name: Role_By_User_Id)</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Role_By_UserDTO</returns>
        public async Task<Role_By_UserDTO> GetRole_By_UserByIdAsync(int id)
        {
            var Role_By_User = await _context.Roles_By_Users.FirstOrDefaultAsync(p => p.Role_By_User_Id == id);

            if (Role_By_User == null)
            {
                return null;
            }

            return MapRole_By_User_To_Role_By_UserDTO(Role_By_User);
        }

        /// <summary>
        /// Updates the record in the Follow_UP table 
        /// </summary>
        /// <param name="role_By_UserDTO">Healthcare.Common.DTO.Role_By_UserDTO</param>
        /// <returns>void</returns>
        public async Task UpdateRole_By_UserAsync(Role_By_UserDTO role_By_UserDTO)
        {
            var role_By_User = await _context.Roles_By_Users.FirstOrDefaultAsync(p => p.Role_By_User_Id == role_By_UserDTO.Role_By_User_Id);

            role_By_User.Role_By_User_Id = role_By_UserDTO.Role_By_User_Id;
            role_By_User.Role_ID = role_By_UserDTO.Role_ID;
            role_By_User.User_ID = role_By_UserDTO.User_ID;

            await _context.SaveChangesAsync();
        }

        #region MAP Role_By_User
        /// <summary>
        /// Maps Healthcare.Common.DTO.Role_By_UserDTO to HealthCare.Data.Entity.Role_By_User
        /// </summary>
        /// <param name="Role_By_UserDto">Healthcare.Common.DTO.Role_By_UserDTO</param>
        /// <returns>HealthCare.Data.Entity.Role_By_User</returns>
        private static Role_By_User MapRole_By_UserDTO_To_Role_By_User(Role_By_UserDTO Role_By_UserDto)
        {
            Role_By_User Role_By_User = new()
            {
                Role_By_User_Id = Role_By_UserDto.Role_By_User_Id,
                Role_ID = Role_By_UserDto.Role_ID,
                User_ID = Role_By_UserDto.User_ID
            };

            return Role_By_User;
        }

        /// <summary>
        /// Maps HealthCare.Data.Entity.Role_By_User to Healthcare.Common.DTO.Role_By_UserDTO
        /// </summary>
        /// <param name="Role_By_User">HealthCare.Data.Entity.Role_By_User</param>
        /// <returns>Healthcare.Common.DTO.Role_By_UserDTO</returns>
        private static Role_By_UserDTO MapRole_By_User_To_Role_By_UserDTO(Role_By_User Role_By_User)
        {
            Role_By_UserDTO Role_By_UserDTO = new()
            {
                Role_By_User_Id = Role_By_User.Role_By_User_Id,
                Role_ID = Role_By_User.Role_ID,
                User_ID = Role_By_User.User_ID
            };

            return Role_By_UserDTO;
        }
        /// <summary>
        /// Maps the data table object
        /// </summary>
        /// <param name="Role_By_Users">IEnumerable of HealthCare.Data.Entity.Role_By_User</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Role_By_UserDTO</returns>
        private static IEnumerable<Role_By_UserDTO> MapRole_By_User_To_Role_By_UserDTO_List(IEnumerable<Role_By_User> Role_By_Users)
        {
            return Role_By_Users.Select(p => MapRole_By_User_To_Role_By_UserDTO(p)).ToList();
        }

        #endregion
    }
}
