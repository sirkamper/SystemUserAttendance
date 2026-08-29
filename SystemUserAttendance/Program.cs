using Microsoft.EntityFrameworkCore;
using SystemUserAttendance.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dodanie bazy danych
builder.Services.AddDbContext<AppDB>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Logika biznesowa
builder.Services.AddScoped<SystemUserAttendance.Services.AttendanceServices, SystemUserAttendance.Services.AttendanceLogic>();

builder.Services.AddScoped<SystemUserAttendance.Services.LeaveRequestServices, SystemUserAttendance.Services.LeaveRequestLogic>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
