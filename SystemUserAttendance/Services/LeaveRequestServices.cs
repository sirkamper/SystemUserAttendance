using SystemUserAttendance.DTOs;
using SystemUserAttendance.Models;

namespace SystemUserAttendance.Services
{
    public interface LeaveRequestServices
    {
        Task<bool> SubmitLeaveRequestAsync(LeaveRequestDTO request);
        Task<IEnumerable<LeaveRequest>> GetEmployeeLeavesAsync(int employeeId);

        Task<bool> UpdateLeaveStatusAsync(int leaveRequestId, LeaveStatus newStatus);
    }
}
