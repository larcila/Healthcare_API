using Healthcare.Common.Action;
using Healthcare.Common.DTO;
using HealthCare.Data.Context;
using HealthCare.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Data.DAL
{
    /// <summary>
    /// Table data Access Layer:  [dbo].[Follow_Ups]
    /// </summary>
    public class Follow_UpDAL : IFollowUpRepository
    {
        private readonly HealthcareDbcontext _context;
        public Follow_UpDAL(HealthcareDbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds information to the table: [dbo].[Follow_Ups]
        /// </summary>
        /// <param name="followUp">Healthcare.Common.DTO.Follow_UpDTO</param>
        /// <returns>void</returns>
        public async Task AddFollow_UpAsync(Follow_UpDTO followUp)
        {
            Follow_Up Follow_Up = MapFollow_UpDTO_To_Follow_Up(followUp);

            await _context.AddAsync(Follow_Up);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes a row from table: Follow_Ups
        /// </summary>
        /// <param name="id">unique identity of the Follow_UP table (Column name: Follow_Up_ID)</param>
        /// <returns>void</returns>
        public async Task DeleteFollow_UpAsync(int id)
        {
            var follow_up = await _context.Follow_Ups.FindAsync(id);
            if (follow_up != null)
            {
                _context.Follow_Ups.Remove(follow_up);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all follow-ups from Follow_Ups table by patient_Id
        /// </summary>
        /// <param name="patient_ID">Id of patient</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Follow_UpDTO</returns>
        public async Task<IEnumerable<Follow_UpDTO>> GetAllFollow_Up_By_PatientAsync(int patient_ID)
        {
            var follow_ups = await _context.Follow_Ups.Where(x => x.Patient_ID == patient_ID).ToListAsync();
            return MapFollow_Up_To_Follow_UpDTO_List(follow_ups);
        }

        /// <summary>
        /// Get follow-ups from table Follow_Ups by unique id
        /// </summary>
        /// <param name="id">unique identity of the Follow_UP table (Column name: Follow_Up_ID)</param>
        /// <returns>Follow_UpDTO</returns>
        public async Task<Follow_UpDTO> GetFollow_UpByIdAsync(int id)
        {
            var follow_Up = await _context.Follow_Ups.FirstOrDefaultAsync(p => p.Follow_Up_ID == id);

            if (follow_Up == null)
            {
                return null;
            }

            return MapFollow_Up_To_Follow_UpDTO(follow_Up);
        }

        /// <summary>
        /// Updates the record in the Follow_UP table 
        /// </summary>
        /// <param name="followUp">Follo-Up data table object</param>
        /// <returns>void</returns>
        public async Task UpdateFollow_UpAsync(Follow_UpDTO followUp)
        {
            var follow_Up = await _context.Follow_Ups.FirstOrDefaultAsync(p => p.Follow_Up_ID == followUp.Follow_Up_ID);

            follow_Up.Allergy = followUp.Allergy;
            follow_Up.Screening = followUp.Screening;
            follow_Up.Symptom = followUp.Symptom;
            follow_Up.Patient_ID = followUp.Patient_ID;
            follow_Up.Registration_Date = followUp.Registration_Date;
            follow_Up.Modify_Date = followUp.Modify_Date;
            follow_Up.Registration_User_ID = followUp.Registration_User_ID;
            follow_Up.Update_User_ID = followUp.Update_User_ID;

            await _context.SaveChangesAsync();
        }

        #region MAP Follow_Up

        /// <summary>
        /// Maps Healthcare.Common.DTO.Follow_upDTO to HealthCare.Data.Entity.Follow_Up
        /// </summary>
        /// <param name="follow_UpDto">Healthcare.Common.DTO.Follow_upDTO</param>
        /// <returns>HealthCare.Data.Entity.Follow_Up</returns>
        private static Follow_Up MapFollow_UpDTO_To_Follow_Up(Follow_UpDTO follow_UpDto)
        {
            Follow_Up follow_Up = new()
            {
                Follow_Up_ID = follow_UpDto.Follow_Up_ID,
                Allergy = follow_UpDto.Allergy,
                Screening = follow_UpDto.Screening,
                Symptom = follow_UpDto.Symptom,
                Patient_ID = follow_UpDto.Patient_ID,
                Registration_Date = follow_UpDto.Registration_Date,
                Modify_Date = follow_UpDto.Modify_Date,
                Registration_User_ID = follow_UpDto.Registration_User_ID,
                Update_User_ID = follow_UpDto.Update_User_ID
            };
            
            return follow_Up;
        }

        /// <summary>
        /// Maps HealthCare.Data.Entity.Follow_Up to Healthcare.Common.DTO.Follow_upDTO
        /// </summary>
        /// <param name="follow_Up">HealthCare.Data.Entity.Follow_Up</param>
        /// <returns>Healthcare.Common.DTO.Follow_upDTO</returns>
        private static Follow_UpDTO MapFollow_Up_To_Follow_UpDTO(Follow_Up follow_Up)
        {
            Follow_UpDTO Follow_UpDTO = new()
            {
                Follow_Up_ID = follow_Up.Follow_Up_ID,
                Allergy = follow_Up.Allergy,
                Screening = follow_Up.Screening,
                Symptom = follow_Up.Symptom,
                Patient_ID = follow_Up.Patient_ID,
                Registration_Date = follow_Up.Registration_Date,
                Modify_Date = follow_Up.Modify_Date,
                Registration_User_ID = follow_Up.Registration_User_ID,
                Update_User_ID = follow_Up.Update_User_ID
            };

            return Follow_UpDTO;
        }

        /// <summary>
        /// Maps the data table object
        /// </summary>
        /// <param name="Follow_Ups">IEnumerable of HealthCare.Data.Entity.Follow_Up</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Follow_UpDTO</returns>
        private static IEnumerable<Follow_UpDTO> MapFollow_Up_To_Follow_UpDTO_List(IEnumerable<Follow_Up> follow_Ups)
        {
            return follow_Ups.Select(p => MapFollow_Up_To_Follow_UpDTO(p)).ToList();
        }

        #endregion
    }
}
