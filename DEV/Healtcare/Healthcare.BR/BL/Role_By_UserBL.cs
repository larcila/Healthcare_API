using Healthcare.Common.Action;
using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.BR.BL
{
    public class Role_By_UserBL : IRole_By_UserBL
    {
        private readonly IRole_By_UserRepository _rol_By_UserRepository;
        private readonly IRoleRepository _rolRepository;


        public Role_By_UserBL(IRole_By_UserRepository rol_By_UserRepository, IRoleRepository RoleRepository)
        {
            _rol_By_UserRepository = rol_By_UserRepository;
            _rolRepository = RoleRepository;
        }

        /// <summary>
        /// Gets all roles for user by user ID.
        /// </summary>
        /// <exception cref="ArgumentException">user_ID must be greater than 0</exception>
        /// <param name="patientId">user_Id</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Role_By_UserDTO</returns>
        public async Task<IEnumerable<Role_By_UserDTO>> GetAllRole_By_User_By_IdUserAsync(int user_ID)
        {
            if (user_ID <= 0)
            {
                throw new ArgumentException("User ID must be greater than 0.", nameof(user_ID));
            }

            return await _rol_By_UserRepository.GetAllRole_By_User_By_IdUserAsync(user_ID);
        }

        /// <summary>
        /// Get role by Role_By_User_ID.
        /// </summary>
        /// <exception cref="ArgumentException">Role_By_User_ID must be greater than 0</exception>
        /// <param name="followUpId">Role_By_User_ID</param>
        /// <returns>Healthcare.Common.DTO.Role_By_UserDTO</returns>
        public async Task<Role_By_UserDTO> GetRole_By_UserByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The Role_By_User_ID must be greater than 0.", nameof(id));
            }

            var rol_by_user = await _rol_By_UserRepository.GetRole_By_UserByIdAsync(id);

            if (rol_by_user == null)
            {
                throw new KeyNotFoundException($"No traceability found with ID {id}.");
            }

            return rol_by_user;
        }

        /// <summary>
        /// Get first role by user id
        /// </summary>
        /// <exception cref="ArgumentException">User_ID must be greater than 0</exception>
        /// <param name="user_ID">User_ID</param>
        /// <returns>Healthcare.Common.DTO.Role_By_UserDTO</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Role_By_UserDTO> GetFirstRole_By_User_By_IdUserAsync(int user_ID)
        {
            if (user_ID <= 0)
            {
                throw new ArgumentException("The User_ID must be greater than 0.", nameof(user_ID));
            }

            IEnumerable<Role_By_UserDTO> list_of_rol_by_user = await GetAllRole_By_User_By_IdUserAsync(user_ID);

            Role_By_UserDTO role_by_user = new();
            if (list_of_rol_by_user.Any())
            {
                role_by_user = list_of_rol_by_user.FirstOrDefault();

                var role = await _rolRepository.GetRolAsync(role_by_user.Role_ID);
                role_by_user.Role = new() {
                    Description = role.Description,
                    Name = role.Name,
                    Role_ID = role.Role_ID
                };
            }

            return role_by_user;
        }

        /// <summary>
        /// Add new Role_By_User
        /// </summary>
        /// <exception cref="ArgumentNullException">role_by_User cannot be null and void.</exception>
        /// <param name="role_By_UserDTO">Healthcare.Common.DTO.Role_By_UserDTO</param>
        /// <returns>void</returns>
        public async Task AddRole_By_UserAsync(Role_By_UserDTO role_By_UserDTO)
        {
            if (role_By_UserDTO == null)
            {
                throw new ArgumentNullException(nameof(role_By_UserDTO), "role_by_User cannot be null and void.");
            }

            IEnumerable<Role_By_UserDTO> roles_by_user = await GetAllRole_By_User_By_IdUserAsync(role_By_UserDTO.User_ID);
            if(roles_by_user.Any())
            {
                foreach (Role_By_UserDTO item in roles_by_user)
                {
                    await DeleteRole_By_UserAsync(item.Role_By_User_Id);
                }
            }
            
            await _rol_By_UserRepository.AddRole_By_UserAsync(role_By_UserDTO);
        }

        /// <summary>
        /// Update an existing Role_By_User.
        /// </summary>
        /// <exception cref="ArgumentNullException">role_by_User cannot be null and void.</exception>
        /// <param name="followUpDto">Healthcare.Common.DTO.Role_By_UserDTO</param>
        /// <returns>void</returns>
        public async Task UpdateRole_By_UserAsync(Role_By_UserDTO role_By_UserDTO)
        {
            if (role_By_UserDTO == null)
            {
                throw new ArgumentNullException(nameof(role_By_UserDTO), "role_By_User cannot be null and void.");
            }

            var existingFollowUp = await _rol_By_UserRepository.GetRole_By_UserByIdAsync(role_By_UserDTO.Role_By_User_Id);
            if (existingFollowUp == null && role_By_UserDTO.User_ID == 0)
            {
                throw new KeyNotFoundException($"No traceability found with ID {role_By_UserDTO.Role_By_User_Id}.");
            }

            if (role_By_UserDTO.User_ID > 0)
            {
                await AddRole_By_UserAsync(role_By_UserDTO);
            } 
            else 
            {
                await _rol_By_UserRepository.UpdateRole_By_UserAsync(role_By_UserDTO);
            }

        }

        /// <summary>
        /// Deletes an existing follow-up by its ID.
        /// </summary>
        /// <param name="id">Role_By_User_ID</param>
        /// <returns>void</returns>
        public async Task DeleteRole_By_UserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The tracking ID must be greater than 0.", nameof(id));
            }

            var existingFollowUp = await _rol_By_UserRepository.GetRole_By_UserByIdAsync(id);
            if (existingFollowUp == null)
            {
                throw new KeyNotFoundException($"No traceability found with ID {id}.");
            }

            await _rol_By_UserRepository.DeleteRole_By_UserAsync(id);
        }
    }
}
