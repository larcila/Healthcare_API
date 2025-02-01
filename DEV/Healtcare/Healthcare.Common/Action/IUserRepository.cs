using Healthcare.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.Action
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetAllUserAsync();
        Task<UserDTO> GetUserByIdAsync(int id, bool needsPassword = false);
        Task AddUserAsync(UserDTO userDTO);
        Task UpdateUserAsync(UserDTO userDTO);
        Task DeleteUserAsync(int id);

        Task<UserDTO> FindUserName(string userName, bool needsPassword = false);
    }
}
