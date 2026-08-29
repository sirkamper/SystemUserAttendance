using SystemUserAttendance.Models;

namespace SystemUserAttendance.Services
{
    public interface AttendanceServices
    {
        Task<bool> CheckInAsync(int employeeId);

        Task<bool> CheckOutAsync(int employeeId);

        Task<bool> UpdateCheckInTimeAsync(int attendanceId, DateTime newTime);

        Task<IEnumerable<Attendance>> GetEmployeeAttendanceAsync(int employeeId);
    }
}
