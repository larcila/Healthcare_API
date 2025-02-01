using Healthcare.Common.Action;
using Healthcare.Common.DTO;
using HealthCare.Data.Context;
using HealthCare.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Data.DAL
{
    public class RoleDAL : IRoleRepository
    {

        private readonly HealthcareDbcontext _context;
        public RoleDAL(HealthcareDbcontext context)
        {
            _context = context;
        }

        public async Task<RoleDTO> GetRolAsync(int idRol)
        {
            var role = await _context.Roles.FindAsync(idRol);
            return MapRol_To_RolDTO(role);
        }


        private static RoleDTO MapRol_To_RolDTO(Role role)
        {
            RoleDTO rolDTO = new()
            {
                 Description =  role.Description,
                 Name = role.Name,
                 Role_ID = role.Role_ID
            };

            return rolDTO;
        }

    }
}
