using Microsoft.AspNetCore.Mvc;
using SystemUserAttendance.DTOs;
using SystemUserAttendance.Services;

namespace SystemUserAttendance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestController :ControllerBase
    {
        private readonly LeaveRequestServices _leaveServices;

        public LeaveRequestController(LeaveRequestServices leaveServices)
        {
            _leaveServices = leaveServices;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitLeaveRequest([FromBody] LeaveRequestDTO request)
        {
            var success = await _leaveServices.SubmitLeaveRequestAsync(request);

            if(!success)
            {
                return BadRequest("Błęd!! Sprawdź poprawność danych");
            }

            return Ok("Wniosek został złozony i oczekuje na zaakcepotowanie");
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeLeaves(int employeeId)
        {
            var leaves = await _leaveServices.GetEmployeeLeavesAsync(employeeId);
            return Ok(leaves);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatus request)
        {
            var success = await _leaveServices.UpdateLeaveStatusAsync(id, request.NewStatus);

            if (!success)
            {
                return NotFound("Błąd!! Nie ma takiego wniosku.");
            }

            return Ok("Status został zaktualizowany");
        }
    }
}
