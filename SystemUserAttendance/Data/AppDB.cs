using SystemUserAttendance.Models;
using Microsoft.EntityFrameworkCore;

namespace SystemUserAttendance.Data
{
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options) : base(options) { }

        //Tabele
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<LeaveRequest> Leaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Jan", LastName = "Kowalski"},
                new Employee { Id = 2, FirstName = "Julian", LastName = "Król" },
                new Employee { Id = 3, FirstName = "Jarosław", LastName = "Kot"},
                new Employee { Id = 4, FirstName = "Donald", LastName = "Kieł" },
                new Employee { Id = 5, FirstName = "Ryszard", LastName = "Nowak" },
                new Employee { Id = 6, FirstName = "Zbyszko", LastName = "Cytryna" },
                new Employee { Id = 7, FirstName = "Adam", LastName = "Kleks" }
                );

        }
    }
}
