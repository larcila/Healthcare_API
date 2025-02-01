using Healthcare.Common.Action;
using Healthcare.Common.DTO;
using HealthCare.Data.Context;
using HealthCare.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Data.DAL
{
    /// <summary>
    /// Table data Access Layer:  [dbo].[Users]
    /// </summary>
    public class UserDAL : IUserRepository
    {
        private readonly HealthcareDbcontext _context;
        public UserDAL(HealthcareDbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds information to the table: [dbo].[Users]
        /// </summary>
        /// <param name="userDTO">Healthcare.Common.DTO.UserDTO</param>
        /// <returns>void</returns>
        public async Task AddUserAsync(UserDTO userDTO)
        {
            User User = MapUserDTO_To_User(userDTO);

            await _context.AddAsync(User);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a row from table: [dbo].[Users]
        /// </summary>
        /// <param name="id">unique identity of the User table (Column name: User_ID)</param>
        /// <returns>void</returns>
        public async Task DeleteUserAsync(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User != null)
            {
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all users from table [dbo].[Users]
        /// </summary>
        /// <returns>IEnumerable of Healthcare.Common.DTO.UserDTO</returns>
        public async Task<IEnumerable<UserDTO>> GetAllUserAsync()
        {
            var Users = await _context.Users.ToListAsync();
            return MapUser_To_UserDTO_List(Users);
        }

        /// <summary>
        /// Get user from table [dbo].[Users] by unique id
        /// </summary>
        /// <param name="id">unique id</param>
        /// <param name="needsPassword">if needsPassword is true returns the value of the Password field</param>
        /// <returns>Healthcare.Common.DTO.UserDTO</returns>
        public async Task<UserDTO> GetUserByIdAsync(int id, bool needsPassword = false)
        {
            var User = await _context.Users.FirstOrDefaultAsync(p => p.User_ID == id);

            if (User == null)
            {
                return null;
            }

            return MapUser_To_UserDTO(User, needsPassword);
        }

        /// <summary>
        /// Updates the record in the Users table 
        /// </summary>
        /// <param name="userDTO">Healthcare.Common.DTO.UserDTO</param>
        /// <returns>void</returns>
        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.User_ID == userDTO.User_ID);

            user.Birth_Date = userDTO.Birth_Date;
            user.Email = userDTO.Email;
            user.Family_Name = userDTO.Family_Name;
            user.First_Name = userDTO.First_Name;
            user.Identification = userDTO.Identification;
            user.User_Name = userDTO.User_Name;
            user.Registration_Date = userDTO.Registration_Date;
            user.Modify_Date = userDTO.Modify_Date;
            user.Registration_User_ID = userDTO.Registration_User_ID;
            user.Update_User_ID = userDTO.Update_User_ID;

            //if the password field arrives with information, it is assumed that it was modified.
            if (!userDTO.Password.IsNullOrEmpty())
            {
                user.Password = userDTO.Password;
            }
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Search by username
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="needsPassword">if needsPassword is true returns the value of the Password field</param>
        /// <returns></returns>
        public async Task<UserDTO> FindUserName(string userName, bool needsPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.User_Name == userName);
            if (user == null)
            {
                return null;
            }
            return MapUser_To_UserDTO(user, needsPassword);
        }


        #region MAP User
        /// <summary>
        /// Maps Healthcare.Common.DTO.UserDTO to HealthCare.Data.Entity.User
        /// </summary>
        /// <param name="UserDto"></param>
        /// <returns></returns>
        private static User MapUserDTO_To_User(UserDTO UserDto)
        {
            User User = new()
            {
                Birth_Date = UserDto.Birth_Date,
                Email = UserDto.Email,
                Family_Name = UserDto.Family_Name,
                First_Name = UserDto.First_Name,
                Identification = UserDto.Identification,
                User_Name = UserDto.User_Name,
                Password = UserDto.Password,
                Registration_Date = UserDto.Registration_Date,
                Modify_Date = UserDto.Modify_Date,
                Registration_User_ID = UserDto.Registration_User_ID,
                Update_User_ID = UserDto.Update_User_ID,
                User_ID = UserDto.User_ID
            };


            return User;
        }

        /// <summary>
        /// Maps HealthCare.Data.Entity.User to Healthcare.Common.DTO.UserDTO
        /// </summary>
        /// <param name="user">HealthCare.Data.Entity.User</param>
        /// <param name="needsPassword">if needsPassword is true returns the value of the Password field</param>
        /// <returns>Healthcare.Common.DTO.UserDTO</returns>
        private static UserDTO MapUser_To_UserDTO(User user, bool needsPassword = false)
        {
            UserDTO UserDTO = new()
            {
                Birth_Date = user.Birth_Date,
                Email = user.Email,
                Family_Name = user.Family_Name,
                First_Name = user.First_Name,
                Identification = user.Identification,
                User_Name = user.User_Name,
                Password = needsPassword ? user.Password : null,
                Registration_Date = user.Registration_Date,
                Modify_Date = user.Modify_Date,
                Registration_User_ID = user.Registration_User_ID,
                Update_User_ID = user.Update_User_ID,
                User_ID = user.User_ID
            };

            return UserDTO;
        }
        /// <summary>
        /// Maps the data table object
        /// </summary>
        /// <param name="Users">IEnumerable of HealthCare.Data.Entity.User</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.UserDTO</returns>
        private static IEnumerable<UserDTO> MapUser_To_UserDTO_List(IEnumerable<User> Users)
        {
            return Users.Select(p => MapUser_To_UserDTO(p)).ToList();
        }

        #endregion
    }
}
