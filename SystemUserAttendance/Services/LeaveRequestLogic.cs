using Microsoft.EntityFrameworkCore;
using SystemUserAttendance.Data;
using SystemUserAttendance.DTOs;
using SystemUserAttendance.Models;

namespace SystemUserAttendance.Services
{
    public class LeaveRequestLogic : LeaveRequestServices
    {
        private readonly AppDB _context;

        public LeaveRequestLogic(AppDB context)
        {
            _context = context;
        }

        public async Task<bool> SubmitLeaveRequestAsync(LeaveRequestDTO request)
        {
            //Sprawdzenie poprawności dat
            if (request.DateFrom > request.DateTo) return false;

            //Czy pracownik istnieje
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == request.EmployeeId);
            if (!employeeExists) return false;

            var leave = new LeaveRequest
            {
                EmployeeId = request.EmployeeId,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                Reason = request.Reason,
                Status = LeaveStatus.Pending
            };

            _context.Leaves.Add(leave);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<LeaveRequest>> GetEmployeeLeavesAsync(int employeeId)
        {
            return await _context.Leaves.Where(l => l.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<bool> UpdateLeaveStatusAsync(int leaveRequestId,  LeaveStatus newStatus)
        {
            //wyszukanie po id
            var leaveRequest = await _context.Leaves.FindAsync(leaveRequestId);

            //Czy wniosek istnieje
            if (leaveRequest == null) return false;

            //Zmiana statusu
            leaveRequest.Status = newStatus;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
