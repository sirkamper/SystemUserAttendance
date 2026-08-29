using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemUserAttendance.Data;
using SystemUserAttendance.Models;

namespace SystemUserAttendance.Services
{
    public class AttendanceLogic : AttendanceServices
    {
        private readonly AppDB _context;

        public AttendanceLogic(AppDB context)
        {
            _context = context;
        }

        public async Task<bool> CheckInAsync(int employeeId)
        {
            //Czy pracownik isntnieje
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeId);
            if (!employeeExists) return false;

            //Czy sesja nie jest otwarta
            var hasOpenSession = await _context.Attendances.AnyAsync(a  => a.EmployeeId == employeeId && a.CheckOutTime == null);

            if (hasOpenSession) return false;

            var attendance = new Attendance
            {
                EmployeeId = employeeId,
                CheckInTime = DateTime.UtcNow
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CheckOutAsync(int  employeeId)
        {
            var openSession = await _context.Attendances.FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.CheckOutTime == null);

            if (openSession == null) return false;

            openSession.CheckOutTime = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCheckInTimeAsync(int attendanceId, DateTime newTime)
        {
            var attendance = await _context.Attendances.FindAsync(attendanceId);

            if (attendance == null) return false; //Sprawdzenie czy wpis obecności istnieje

            if (newTime > DateTime.Now) return false; //Poprawność daty

            if (attendance.CheckOutTime.HasValue && newTime > attendance.CheckOutTime.Value) return false;

            attendance.CheckInTime = newTime;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Attendance>> GetEmployeeAttendanceAsync(int employeeId)
        {
            return await _context.Attendances.Where(a => a.EmployeeId == employeeId).OrderByDescending(a => a.CheckInTime).ToListAsync();
        }
    }
}
