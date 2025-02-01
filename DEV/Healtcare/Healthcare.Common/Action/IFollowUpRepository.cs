using Healthcare.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.Action
{
    public interface IFollowUpRepository
    {
        Task<IEnumerable<Follow_UpDTO>> GetAllFollow_Up_By_PatientAsync(int patientId);
        Task<Follow_UpDTO> GetFollow_UpByIdAsync(int followUpId);
        Task AddFollow_UpAsync(Follow_UpDTO followUpDto);
        Task UpdateFollow_UpAsync(Follow_UpDTO followUpDto);
        Task DeleteFollow_UpAsync(int followUpId);
    }
}
