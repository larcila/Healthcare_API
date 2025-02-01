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
    public class Role_By_User
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
        public User? User { get; set; }
        /// <summary>
        /// Navigation
        /// </summary>
        public Role? Role { get; set; }
    }
}
