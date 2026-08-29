using SystemUserAttendance.DTOs;
using SystemUserAttendance.Models;

namespace SystemUserAttendance.Services
{
    public interface LeaveRequestServices
    {
        Task<bool> SubmitLeaveRequestAsync(LeaveRequestDTO request);
        Task<IEnumerable<LeaveRequest>> GetLeavesAsync(int? employeeId);

        Task<bool> ApproveLeaveAsync(int id);
        Task<bool> RejectLeaveAsync(int id);
    }
}
