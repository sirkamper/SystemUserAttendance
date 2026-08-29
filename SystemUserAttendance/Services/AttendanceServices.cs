namespace SystemUserAttendance.Services
{
    public interface AttendanceServices
    {
        Task<bool> CheckInAsync(int employeeId);

        Task<bool> CheckOutAsync(int employeeId);
    }
}
