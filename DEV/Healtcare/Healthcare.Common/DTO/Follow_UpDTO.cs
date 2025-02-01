using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.DTO
{
    public class Follow_UpDTO
    {
        /// <summary>
        /// unique identification of the registry on database
        /// </summary>
        public int Follow_Up_ID { get; set; }
        /// <summary>
        /// Allergy e.g.
        /// Pet Allergies, Food Allergies, Pollen Allergies (Hay Fever)
        /// </summary>
        public required string Allergy { get; set; }
        /// <summary>
        /// Symptoms: Hives, stomach cramps, vomiting, diarrhea, swelling, trouble breathing.
        /// Symptoms: Sneezing, nasal congestion, itchy eyes and throat, watery eyes
        /// </summary>
        public required string Symptom { get; set; }
        /// <summary>
        /// Screening: Skin prick test, blood tests
        /// Screening: Skin prick test, blood tests, mold-specific IgE test.
        /// </summary>
        public required string Screening { get; set; }
        /// <summary>
        /// Field relating to patient information
        /// </summary>
        public int Patient_ID { get; set; }
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
        //public required PatientDTO Patient { get; set; }
    }
}
