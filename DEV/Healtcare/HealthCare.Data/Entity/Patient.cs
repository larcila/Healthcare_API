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
    public class Patient
    {
        /// <summary>
        /// unique identification of the registry on database
        /// </summary>
        public int Patient_ID { get; set; }
        /// <summary>
        /// Patient first name
        /// </summary>
        public required string First_Name { get; set; }
        /// <summary>
        /// patient surname or family name
        /// </summary>
        public required string Family_Name { get; set; }
        /// <summary>
        /// patient identification
        /// </summary>
        public required string Identification { get; set; }
        /// <summary>
        /// patient birth date
        /// </summary>
        public DateTime Birth_Date { get; set; }
        /// <summary>
        /// patient email
        /// </summary>
        public required string Email { get; set; }
        /// <summary>
        /// patient phone number
        /// </summary>
        public string? Phone_Number { get; set; }
        /// <summary>
        /// Date on which the information is entered for the first time
        /// </summary>
        public DateTime Registration_Date { get; set; }
        /// <summary>
        /// Date on which the information is changed
        /// </summary>
        public DateTime? Modify_Date { get; set; }
        /// <summary>
        /// User who enters the information for the first time
        /// </summary>
        public int Registration_User_ID { get; set; }
        /// <summary>
        /// user who modifies the information
        /// </summary>
        public int? Update_User_ID { get; set; }
        /// <summary>
        /// Navigation
        /// </summary>
        public IEnumerable<Follow_Up>? Follow_Up_By_Patient { get; set; }
    }
}
