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
    /// Table data Access Layer:  [dbo].[Patients]
    /// </summary>
    public class PatientDAL : IPatientRepository
    {
        private readonly HealthcareDbcontext _context;
        public PatientDAL(HealthcareDbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds information to the table: [dbo].[Patient]
        /// </summary>
        /// <param name="patientDto">Patient data table object</param>
        /// <returns>void</returns>
        public async Task AddPatientAsync(PatientDTO patientDto)
        {
            Patient patient = MapPatientDTO_To_Patient(patientDto);

            await _context.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a row from table: [dbo].[Patient]
        /// </summary>
        /// <param name="id">unique identity of the Patient table (Column name: Follow_Up_ID)</param>
        /// <returns>void</returns>
        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all patients from [dbo].[Patient] table
        /// </summary>
        /// <returns>IEnumerable of PatientDTO </returns>
        public async Task<IEnumerable<PatientDTO>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return MapPatient_To_PatientDTO_List(patients);
        }

        /// <summary>
        /// Get petient from [dbo].[Patient] table by unique id
        /// </summary>
        /// <param name="id">unique id (column name: Patient_ID)</param>
        /// <returns>PatientDTO</returns>
        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Patient_ID == id);

            if (patient == null)
            {
                return null;
            }

            return MapPatient_To_PatientDTO(patient);
        }

        #region MAP Patient
        /// <summary>
        /// Updates the record in the [dbo].[Patient] table 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patientDto">Healthcare.Common.DTO.PatientDTO</param>
        /// <returns>void</returns>
        public async Task UpdatePatientAsync(int id, PatientDTO patientDto)
        {
            var patientToEdit = await _context.Patients.FirstOrDefaultAsync(p => p.Patient_ID == id);
            
            patientToEdit.Family_Name = patientDto.Family_Name;
            patientToEdit.First_Name = patientDto.First_Name;
            patientToEdit.Email = patientDto.Email;
            patientToEdit.Identification = patientDto.Identification;
            patientToEdit.Phone_Number = patientDto.Phone_Number;
            patientToEdit.Birth_Date = patientDto.Birth_Date;

            patientToEdit.Update_User_ID= patientDto.Update_User_ID;
            patientToEdit.Modify_Date = patientDto.Modify_Date;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Maps Healthcare.Common.DTO.PatientDTO to HealthCare.Data.Entity.Patient.Patient
        /// </summary>
        /// <param name="patientDto">Healthcare.Common.DTO.PatientDTO</param>
        /// <returns>HealthCare.Data.Entity.Patient.Patient</returns>
        private static Patient MapPatientDTO_To_Patient(PatientDTO patientDto)
        {
            Patient patient = new()
            {
                Patient_ID = patientDto.Patient_ID,
                Family_Name = patientDto.Family_Name,
                First_Name = patientDto.First_Name,
                Email = patientDto.Email,
                Identification = patientDto.Identification,
                Phone_Number = patientDto.Phone_Number,
                Birth_Date = patientDto.Birth_Date,
                
                Registration_Date = patientDto.Registration_Date,
                Modify_Date = patientDto.Modify_Date,
                Registration_User_ID = patientDto.Registration_User_ID,
                Update_User_ID = patientDto.Update_User_ID
            };

            return patient;
        }

        /// <summary>
        /// Maps HealthCare.Data.Entity.Patient.Patient to Healthcare.Common.DTO.PatientDTO
        /// </summary>
        /// <param name="patient">HealthCare.Data.Entity.Patient.Patient</param>
        /// <returns>Healthcare.Common.DTO.PatientDTO</returns>
        private static PatientDTO MapPatient_To_PatientDTO(Patient patient)
        {
            PatientDTO patientDTO = new()
            {
                Patient_ID = patient.Patient_ID,
                Family_Name = patient.Family_Name,
                First_Name = patient.First_Name,
                Email = patient.Email,
                Identification = patient.Identification,
                Phone_Number = patient.Phone_Number,
                Birth_Date = patient.Birth_Date,
                
                Registration_Date = patient.Registration_Date,
                Modify_Date = patient.Modify_Date,
                Registration_User_ID = patient.Registration_User_ID,
                Update_User_ID = patient.Update_User_ID
                  
            };

            return patientDTO;
        }

        /// <summary>
        /// Maps the data table object
        /// </summary>
        /// <param name="patients">IEnumerable of HealthCare.Data.Entity.Patient</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.PatientDTO</returns>
        private static IEnumerable<PatientDTO> MapPatient_To_PatientDTO_List(IEnumerable<Patient> patients)
        {
            return patients.Select(p => MapPatient_To_PatientDTO(p)).ToList();
        }

        #endregion

    }
}
