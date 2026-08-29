using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.Configuration;
using SystemUserAttendance.DTOs;
using SystemUserAttendance.Services;

namespace SystemUserAttendance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceServices _attendanceServices; 

        public AttendanceController(AttendanceServices attendanceServices)
        {
            _attendanceServices = attendanceServices;
        }

        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] CheckIn request)
        {
            var success = await _attendanceServices.CheckInAsync(request.EmployeeId);

            if (!success)
            {
                return BadRequest("Błąd!! Pracownik ma już otwarta sesję lub wprowadzono błedne dane. ");
            }

            return Ok("Wejście zarejestrowane.");
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckIn request)
        {
            var success = await _attendanceServices.CheckOutAsync(request.EmployeeId);

            if (!success)
            {
                return BadRequest("Błąd!! Brak otwartej sesji dla tego pracownika.");
            }

            return Ok("Wyjście zarejestrowane.");
        }

        [HttpPut("{attendanceId}")]
        public async Task<IActionResult> UpdateCheckIn(int attendanceId, [FromBody] UpdateAttendance request)
        {
            var success = await _attendanceServices.UpdateCheckInTimeAsync(attendanceId, request.NewCheckInTime);

            if (!success)
            {
                return BadRequest("Błąd!! Błędna godzina lub dane użytkownika");
            }

            return Ok("Godzina wejścia została zaktualizowana.");
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetAttendanceHistory(int employeeId)
        {
            var history = await _attendanceServices.GetEmployeeAttendanceAsync(employeeId);
            return Ok(history);
        }
    }
}
