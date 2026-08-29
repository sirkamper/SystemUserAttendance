namespace SystemUserAttendance.DTOs
{
    public class LeaveRequestDTO
    {
        public int EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
