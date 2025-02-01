using Healthcare.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.Action
{
    public interface IRoleRepository
    {
        Task<RoleDTO> GetRolAsync(int idRol);
    }
}
