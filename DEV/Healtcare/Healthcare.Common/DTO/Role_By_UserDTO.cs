using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.DTO
{
    /// <summary>
    /// Data table object
    /// </summary>
    public class Role_By_UserDTO
    {
        /// <summary>
        /// unique identification of the registry
        /// </summary>
        public int Role_By_User_Id { get; set; }
        /// <summary>
        /// User unique identification on database
        /// </summary>
        public int User_ID { get; set; }
        /// <summary>
        /// Role unique identification on database
        /// </summary>
        public int Role_ID { get; set; }
        /// <summary>
        /// Navigation
        /// </summary>
        //public required User User { get; set; }
        /// <summary>
        /// Navigation
        /// </summary>
        public RoleDTO? Role { get; set; }
    }

    /// <summary>
    /// Data table object
    /// Applies only to the controller
    /// </summary>
    public class AddOrUpdateRol  
    {
        public int Role_By_User_Id { get; set; }
        /// <summary>
        /// User unique identification on database
        /// </summary>
        public int User_ID { get; set; }
        /// <summary>
        /// Role unique identification on database
        /// </summary>
        public int Role_ID { get; set; }
    }

}
