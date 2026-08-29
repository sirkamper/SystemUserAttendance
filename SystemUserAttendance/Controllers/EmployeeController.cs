using Microsoft.AspNetCore.Mvc;
using SystemUserAttendance.Data;
using SystemUserAttendance.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SystemUserAttendance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDB _context;

            public EmployeeController (AppDB context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployee()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                })
                .ToListAsync();

            return Ok(employees);
        }
    }
}
