using Healthcare.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.ActionBL
{
    public interface IPatientBL
    {
        Task AddPatientAsync(PatientDTO patientDto);
        Task<PatientDTO> GetPatientByIdAsync(int id);
        Task<IEnumerable<PatientDTO>> GetAllPatientsAsync();
        Task UpdatePatientAsync(int id, PatientDTO patientDto);
        Task DeletePatientAsync(int id);
    }
}
