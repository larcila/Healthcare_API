using Healthcare.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Common.ActionBL
{
    public interface IFollowUpBL
    {
        Task<IEnumerable<Follow_UpDTO>> GetAllFollowUpsPatientAsync(int patientId);
        Task<Follow_UpDTO> GetFollowUpByIdAsync(int followUpId);
        Task AddFollowUpAsync(Follow_UpDTO followUpDto);
        Task UpdateFollowUpAsync(Follow_UpDTO followUpDto);
        Task DeleteFollowUpAsync(int followUpId);
    }
}
