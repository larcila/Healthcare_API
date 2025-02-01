using Healthcare.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.ActionBL
{
    public interface IRole_By_UserBL
    {
        Task<IEnumerable<Role_By_UserDTO>> GetAllRole_By_User_By_IdUserAsync(int user_ID);
        Task<Role_By_UserDTO> GetRole_By_UserByIdAsync(int id);
        Task AddRole_By_UserAsync(Role_By_UserDTO role_By_UserDTO);
        Task UpdateRole_By_UserAsync(Role_By_UserDTO role_By_UserDTO);
        Task DeleteRole_By_UserAsync(int id);
        Task<Role_By_UserDTO> GetFirstRole_By_User_By_IdUserAsync(int user_ID);
    }
}
