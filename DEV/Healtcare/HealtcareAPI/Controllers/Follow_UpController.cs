using Healthcare.BR.BL;
using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;
using HealthCare.Data.DAL;
using HealthCare.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareAPI.Controllers
{
    /// <summary>
    /// Patient follow-up 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Follow_UpController : ControllerBase
    {

        private readonly IFollowUpBL _follow_UpBL;

        public Follow_UpController(IFollowUpBL follow_UpBL)
        {
            _follow_UpBL = follow_UpBL;
        }


        /// <summary>
        /// Get follow-ups by patient
        /// </summary>
        /// <param name="idPatient">idPatient</param>
        /// <returns>IEnumerable of Healthcare.Common.DTO.Follow_UpDTO</returns>
        // GET: api/<Follow_UpController>
        [HttpGet("patient/{idPatient}")]
        public async Task<IActionResult> GetAllFollow_Up(int idPatient)
        {
            var follow_Up = await _follow_UpBL.GetAllFollowUpsPatientAsync(idPatient);
            return Ok(follow_Up);
        }

        /// <summary>
        /// Get an specific follow-up
        /// </summary>
        /// <param name="id">Unique identify of the follow-up</param>
        /// <returns>Healthcare.Common.DTO.Follow_UpDTO</returns>
        // GET api/<Follow_UpController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollow_UpById(int id)
        {
            var follow_Up = await _follow_UpBL.GetFollowUpByIdAsync(id);
            return Ok(follow_Up);
        }

        /// <summary>
        /// Add new follow-up from patient in database
        /// </summary>
        /// <param name="follow_UpDto">Healthcare.Common.DTO.Follow_UpDTO</param>
        /// <returns>void</returns>
        // POST api/<Follow_UpController>
        [HttpPost]
        public async Task<IActionResult> AddFollow_Up([FromBody] Follow_UpDTO follow_UpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _follow_UpBL.AddFollowUpAsync(follow_UpDto);
            return CreatedAtAction(nameof(GetFollow_UpById), new { id = follow_UpDto.Follow_Up_ID }, follow_UpDto);
        }

        /// <summary>
        /// Modify or edit follow-up
        /// </summary>
        /// <param name="follow_UpDto">Healthcare.Common.DTO.Follow_UpDTO</param>
        /// <returns>void</returns>
        // PUT api/<Follow_UpController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateFollow_Up([FromBody] Follow_UpDTO follow_UpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPatient = await _follow_UpBL.GetFollowUpByIdAsync(follow_UpDto.Follow_Up_ID);
            if (existingPatient == null)
            {
                return NotFound(new { Message = "Follow_up not found" });
            }

            await _follow_UpBL.UpdateFollowUpAsync(follow_UpDto);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing follow-up
        /// </summary>
        /// <param name="id">Follow_Up_ID</param>
        /// <returns>void</returns>
        // DELETE api/<Follow_UpController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollow_Up(int id)
        {
            var existingPatient = await _follow_UpBL.GetFollowUpByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound(new { Message = "Patient not found" });
            }

            await _follow_UpBL.DeleteFollowUpAsync(id);
            return NoContent();
        }
    }
}
