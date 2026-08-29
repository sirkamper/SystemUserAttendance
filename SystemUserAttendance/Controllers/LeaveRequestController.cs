using Microsoft.AspNetCore.Mvc;
using SystemUserAttendance.DTOs;
using SystemUserAttendance.Services;

namespace SystemUserAttendance.Controllers
{
    [ApiController]
    [Route("api/leaves")]
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

        [HttpGet]
        public async Task<IActionResult> GetLeaves([FromQuery] int? employeeId)
        {
            var leaves = await _leaveServices.GetLeavesAsync(employeeId);
            return Ok(leaves);
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> approveLeave(int id)
        {
            var success = await _leaveServices.ApproveLeaveAsync(id);

            if (!success)
            {
                return NotFound("Błąd!! Nie ma takiego wniosku lub nie został przeznaczony do rozpatrzenia.");
            }

            return Ok("Urlop został zatwierdzony.");
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectLeave(int id)
        {
            var success = await _leaveServices.RejectLeaveAsync(id);
            if (!success) return BadRequest("Błąd!! Nie ma takiego wniosku lub nie został przeznaczony do rozpatrzenia.");
            return Ok("Urlop został odrzucony.");
        }
    }
}
