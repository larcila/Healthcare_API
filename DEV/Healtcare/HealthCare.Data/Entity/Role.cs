using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Data.Entity
{
    /// <summary>
    /// Data table object
    /// </summary>
    public class Role
    {
        /// <summary>
        /// unique identification of the registry on database
        /// </summary>
        public int Role_ID { get; set; }
        /// <summary>
        /// Role name
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Role description
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        /// Navigation
        /// </summary>
        public ICollection<Role_By_User>? User_By_Role { get; set; }

    }
}
