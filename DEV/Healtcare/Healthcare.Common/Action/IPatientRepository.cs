using Healthcare.Common.DTO;


namespace Healthcare.Common.Action
{
    public interface IPatientRepository
    {
        Task AddPatientAsync(PatientDTO patientDto);
        Task<PatientDTO> GetPatientByIdAsync(int id);
        Task<IEnumerable<PatientDTO>> GetAllPatientsAsync();
        Task UpdatePatientAsync(int id, PatientDTO patientDto);
        Task DeletePatientAsync(int id);
    }
}
