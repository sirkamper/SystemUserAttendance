namespace SystemUserAttendance.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } //Klucz
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending; //Domyslnie na nierozstrzygnięty

        public Employee? Employee { get; set; } //Nawigacja

    }
}
