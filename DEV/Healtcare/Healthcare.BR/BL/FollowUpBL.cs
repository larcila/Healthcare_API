using Healthcare.Common.Action;
using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;

namespace Healthcare.BR.BL
{
    public class FollowUpBL : IFollowUpBL
    {
        private readonly IFollowUpRepository _followUpRepository;

        public FollowUpBL(IFollowUpRepository followUpRepository)
        {
            _followUpRepository = followUpRepository;
        }

        /// <summary>
        /// Gets all follow-up queries for a patient by patient ID.
        /// </summary>
        /// <exception cref="ArgumentException">patientId must be greater than 0</exception>
        /// <param name="patientId">Patient ID</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Follow_UpDTO</returns>
        public async Task<IEnumerable<Follow_UpDTO>> GetAllFollowUpsPatientAsync(int patientId)
        {
            if (patientId <= 0)
            {
                throw new ArgumentException("Patient ID must be greater than 0.", nameof(patientId));
            }

            return await _followUpRepository.GetAllFollow_Up_By_PatientAsync(patientId);
        }

        /// <summary>
        /// Get patient tracking by Follow_Up_ID.
        /// </summary>
        /// <exception cref="ArgumentException">The followUpId must be greater than 0.</exception>
        /// <param name="followUpId">Follow_Up_ID</param>
        /// <returns>Healthcare.Common.DTO.Follow_UpDTO</returns>
        public async Task<Follow_UpDTO> GetFollowUpByIdAsync(int followUpId)
        {
            if (followUpId <= 0)
            {
                throw new ArgumentException("The tracking ID must be greater than 0.", nameof(followUpId));
            }

            var followUp = await _followUpRepository.GetFollow_UpByIdAsync(followUpId);

            if (followUp == null)
            {
                throw new KeyNotFoundException($"No traceability found with ID {followUpId}.");
            }

            return followUp;
        }

        /// <summary>
        /// Add a new follow-up.
        /// </summary>
        /// <exception cref="ArgumentNullException">Follow-up cannot be null and void.</exception>
        /// <param name="followUpDto">Healthcare.Common.DTO.Follow_UpDTO</param>
        /// <returns>void</returns>
        public async Task AddFollowUpAsync(Follow_UpDTO followUpDto)
        {
            if (followUpDto == null)
            {
                throw new ArgumentNullException(nameof(followUpDto), "Follow-up cannot be null and void.");
            }

            followUpDto.Registration_Date = DateTime.UtcNow;
            await _followUpRepository.AddFollow_UpAsync(followUpDto);
        }

        /// <summary>
        /// Update an existing follow-up.
        /// </summary>
        /// <exception cref="ArgumentNullException">Healthcare.Common.DTO.Follow_UpDTO cannot be null and void.</exception>
        /// <param name="followUpDto">Healthcare.Common.DTO.Follow_UpDTO</param>
        /// <returns>void</returns>
        public async Task UpdateFollowUpAsync(Follow_UpDTO followUpDto)
        {
            if (followUpDto == null)
            {
                throw new ArgumentNullException(nameof(followUpDto), "Follow-up cannot be null and void.");
            }

            var existingFollowUp = await _followUpRepository.GetFollow_UpByIdAsync(followUpDto.Follow_Up_ID);
            if (existingFollowUp == null)
            {
                throw new KeyNotFoundException($"No traceability found with ID {followUpDto.Follow_Up_ID}.");
            }

            followUpDto.Modify_Date = DateTime.UtcNow;
            await _followUpRepository.UpdateFollow_UpAsync(followUpDto);
        }

        /// <summary>
        /// Deletes an existing follow-up by its ID.
        /// </summary>
        /// <exception cref="ArgumentException">The tracking ID must be greater than 0.</exception>
        /// <param name="followUpId">followUpId/param>
        /// <returns>void</returns>
        public async Task DeleteFollowUpAsync(int followUpId)
        {
            if (followUpId <= 0)
            {
                throw new ArgumentException("The tracking ID must be greater than 0.", nameof(followUpId));
            }

            var existingFollowUp = await _followUpRepository.GetFollow_UpByIdAsync(followUpId);
            if (existingFollowUp == null)
            {
                throw new KeyNotFoundException($"No traceability found with ID {followUpId}.");
            }

            await _followUpRepository.DeleteFollow_UpAsync(followUpId);
        }
    }
}
