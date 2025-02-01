using Healthcare.Common.Action;
using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;

namespace Healthcare.BR.BL
{
    public class PatientBL : IPatientBL
    {
        private readonly IPatientRepository _patientRepository;

        public PatientBL(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Add a new patient.
        /// </summary>
        /// <exception cref="ArgumentNullException">Patient cannot be null and void.</exception>
        /// <param name="patientDto">Healthcare.Common.DTO.PatientDTO</param>
        /// <returns>void</returns>
        public async Task AddPatientAsync(PatientDTO patientDto)
        {
            if (patientDto == null)
            {
                throw new ArgumentNullException(nameof(patientDto), "Patient cannot be null and void.");
            }

            // Lógica adicional (validación, reglas, etc.)
            await _patientRepository.AddPatientAsync(patientDto);
        }

        /// <summary>
        /// Obtains patient by Id.
        /// </summary>
        /// <exception cref="ArgumentException">The id must be greater than 0.</exception>
        /// <param name="id">unique id</param>
        /// <returns>Healthcare.Common.DTO.PatientDTO</returns>
        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The tracking ID must be greater than 0.", nameof(id));
            }

            // Lógica adicional si aplica
            return await _patientRepository.GetPatientByIdAsync(id);
        }

        /// <summary>
        /// Gets all patients.
        /// </summary>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Follow_UpDTO</returns>
        public async Task<IEnumerable<PatientDTO>> GetAllPatientsAsync()
        {
            // Lógica adicional si aplica
            return await _patientRepository.GetAllPatientsAsync();
        }

        /// <summary>
        /// Update an existing patient.
        /// </summary>
        /// <param name="id">Patient id</param>
        /// <param name="patientDto">Healthcare.Common.DTO.PatientDTO</param>
        /// <returns>void</returns>
        public async Task UpdatePatientAsync(int id, PatientDTO patientDto)
        {
            // Lógica adicional antes de actualizar
            await _patientRepository.UpdatePatientAsync(id, patientDto);
        }

        /// <summary>
        /// Deletes an existing patient by its ID.
        /// </summary>
        /// <param name="id">Patient id</param>
        /// <returns>void</returns>
        public async Task DeletePatientAsync(int id)
        {
            // Lógica adicional (e.g., verificaciones antes de eliminar)
            await _patientRepository.DeletePatientAsync(id);
        }
    }
}
