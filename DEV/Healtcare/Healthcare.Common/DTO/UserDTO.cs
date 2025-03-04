﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.DTO
{
    /// <summary>
    /// Data table object
    /// </summary>
    public class UserDTO
    {
        /// unique identification of the registry
        /// </summary>
        public int User_ID { get; set; }
        /// <summary>
        /// User first name
        /// </summary>
        public required string First_Name { get; set; }
        /// <summary>
        /// User surname or family name
        /// </summary>
        public required string Family_Name { get; set; }
        /// <summary>
        /// user identification
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
        /// user name
        /// </summary>
        public required string User_Name { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string? Password { get; set; }
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

        //public ICollection<Role_By_User>? Role_By_Users { get; set; }
    }

    public class AddUpdateUserDTO
    {
        public int User_ID { get; set; }
        public required string First_Name { get; set; }
        public required string Family_Name { get; set; }
        public required string Identification { get; set; }
        public DateTime Birth_Date { get; set; }
        public required string Email { get; set; }
        public required string User_Name { get; set; }
        public string Password { get; set; }
        public DateTime Registration_Date { get; set; }
        public DateTime? Modify_Date { get; set; }
        public int Registration_User_ID { get; set; }
        public int? Update_User_ID { get; set; }

        public AddOrUpdateRol? Role_By_Users { get; set; }
    }
}
