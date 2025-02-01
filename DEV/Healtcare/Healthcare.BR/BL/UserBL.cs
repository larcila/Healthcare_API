using Healthcare.Common.Action;
using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace Healthcare.BR.BL
{
    public class UserBL: IUserBL
    {
        private readonly IUserRepository _userRepository;

        public UserBL(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>IEnumerable of Healthcare.Common.DTO.UserDTO</returns>
        public async Task<IEnumerable<UserDTO>> GetAllUserAsync()
        {
            return await _userRepository.GetAllUserAsync();
        }

        /// <summary>
        /// Get users searching by User_Id.
        /// </summary>
        /// <exception cref="ArgumentException">The user id must be greater than 0.</exception>
        /// <param name="id">User_ID</param>
        /// <returns>Healthcare.Common.DTO.UserDTO.UserDTO</returns>
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The user ID must be greater than 0.", nameof(id));
            }

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"No traceability found with ID {id}.");
            }

            return user;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <exception cref="ArgumentNullException">user cannot be null and void.</exception>
        /// <param name="userDto">Healthcare.Common.DTO.UserDTO.UserDTO</param>
        /// <returns>void</returns>
        public async Task AddUserAsync(UserDTO userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "user cannot be null and void.");
            }

            if (await UserNameExists(userDto))
            {
                throw new KeyNotFoundException($"user name exists, change it to a non-existing one {userDto.User_Name}.");
            }

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                userDto.Password = HashPassword(userDto.Password); // Encripta la contraseña
            }

            userDto.Registration_Date = DateTime.UtcNow;
            await _userRepository.AddUserAsync(userDto);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <exception cref="ArgumentNullException">Follow-up cannot be null and void.</exception>
        /// <param name="userDto">Healthcare.Common.DTO.UserDTO.UserDTO</param>
        /// <returns>void</returns>
        public async Task UpdateUserAsync(UserDTO userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "Follow-up cannot be null and void.");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(userDto.User_ID);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"No traceability found with ID {userDto.User_ID}.");
            }

            if (await UserNameExists(userDto))
            {
                throw new KeyNotFoundException($"user name exists, change it to a non-existing one {userDto.User_Name}.");
            }


            if (!string.IsNullOrWhiteSpace(userDto.Password) && !userDto.Password.IsNullOrEmpty())
            {
                userDto.Password = HashPassword(userDto.Password); // Encripta la contraseña
            }

            userDto.Modify_Date = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(userDto);
        }

        /// <summary>
        /// Deletes an existing user by its ID.
        /// </summary>
        /// <param name="id">User_ID</param>
        /// <returns>void</returns>
        public async Task DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The tracking ID must be greater than 0.", nameof(id));
            }

            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"No traceability found with ID {id}.");
            }

            await _userRepository.DeleteUserAsync(id);
        }

        /// <summary>
        /// Find user by user_name
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="needsPassword">if needsPassword is true returns the value of the Password field</param>
        /// <returns>Healthcare.Common.DTO.UserDTO.UserDTO</returns>
        public async Task<UserDTO> FindUserName(string userName, bool needsPassword = false)
        {
            return await _userRepository.FindUserName(userName, needsPassword);
        }

        /// <summary>
        /// Validates if the user name is already registered.
        /// </summary>
        /// <param name="userDto">Healthcare.Common.DTO.UserDTO.UserDTO</param>
        /// <returns>bool</returns>
        private async Task<bool> UserNameExists(UserDTO userDto)
        {
            IEnumerable<UserDTO> users = await _userRepository.GetAllUserAsync();
            return users.Any(item => item.User_Name == userDto.User_Name && item.User_ID != userDto.User_ID);
        }


        #region HASH PASSWORD

        /// <summary>
        /// Generates a secure hash for the password.
        /// </summary>
        /// <param name="password">password to store in database</param>
        /// <returns>encrypted password</returns>
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Checks if a password matches its hash.
        /// </summary>
        /// <param name="password">Contraseña original</param>
        /// <param name="hashedPassword">Hash stored in database</param>
        /// <returns>True if they match; False otherwise</returns>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
        #endregion

    }
}
