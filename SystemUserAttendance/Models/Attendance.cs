namespace SystemUserAttendance.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } //Klucz
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public Employee? Employee { get; set; } //Nawigacja
    }
}
