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

            //Czy pracownik ma wniosek
            var hasOverLappingLeave = await _context.Leaves.AnyAsync(l => l.EmployeeId == request.EmployeeId &&
                (l.Status == LeaveStatus.Pending || l.Status == LeaveStatus.Approved) && (request.DateFrom <= l.DateTo && request.DateTo >= l.DateFrom));

            if (hasOverLappingLeave) return false;

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

        public async Task<IEnumerable<LeaveRequest>> GetLeavesAsync(int? employeeId)
        {
            var query = _context.Leaves.AsQueryable();

            if (employeeId.HasValue)
            {
                query = query.Where(l => l.EmployeeId == employeeId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> ApproveLeaveAsync(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);

            //Warunek rozpatrywania wniosku
            if (leave == null || leave.Status != LeaveStatus.Pending) return false;

            leave.Status = LeaveStatus.Approved;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectLeaveAsync(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            //Warunek rozpatrywania wniosku
            if (leave == null || leave.Status != LeaveStatus.Pending) return false;

            leave.Status = LeaveStatus.Rejected;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
