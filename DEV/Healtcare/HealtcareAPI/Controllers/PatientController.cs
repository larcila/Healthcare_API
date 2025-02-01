using Healthcare.Common.Action;
using Healthcare.Common.ActionBL;
using Healthcare.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealtcareAPI.Controllers
{

    /// <summary>
    /// Patient service
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientBL _patientBL;

        public PatientController(IPatientBL patientBL)
        {
            _patientBL = patientBL;
        }

        /// <summary>
        /// List all patient
        /// </summary>
        /// <returns></returns>
        // GET: api/Patient
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientBL.GetAllPatientsAsync();
            return Ok(patients);
        }

        /// <summary>
        /// Get the patient by filtering by id
        /// </summary>
        /// <param name="id">is the unique ID in the table</param>
        /// <returns>Healthcare.Common.DTO.PatientDTO</returns>
        // GET: api/Patient/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _patientBL.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound(new { Message = "Patient not found" });
            }
            return Ok(patient);
        }

        /// <summary>
        /// Add new patient in database
        /// </summary>
        /// <param name="patientDto">Healthcare.Common.DTO.PatientDTO</param>
        /// <returns>void</returns>
        // POST: api/Patient
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientDTO patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _patientBL.AddPatientAsync(patientDto);
            return CreatedAtAction(nameof(GetPatientById), new { id = patientDto.Patient_ID }, patientDto);
        }

        /// <summary>
        /// Modify or edit patient
        /// </summary>
        /// <param name="patientDto">Healthcare.Common.DTO.PatientDTO</param>
        /// <returns>NoContent()</returns>
        // PUT: api/Patient/
        [HttpPut]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientDTO patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPatient = await _patientBL.GetPatientByIdAsync(patientDto.Patient_ID);
            if (existingPatient == null)
            {
                return NotFound(new { Message = "Patient not found" });
            }

            await _patientBL.UpdatePatientAsync(patientDto.Patient_ID, patientDto);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing patient
        /// </summary>
        /// <param name="id">Patient_ID</param>
        /// <returns>NoContent()</returns>
        // DELETE: api/Patient/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var existingPatient = await _patientBL.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound(new { Message = "Patient not found" });
            }

            await _patientBL.DeletePatientAsync(id);
            return NoContent();
        }
    }
}
