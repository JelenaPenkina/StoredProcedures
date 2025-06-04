using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoredProcedure.Data;
using StoredProcedure.Models;

namespace StoredProcedure.Controllers;

public class EmployeeController : Controller
{
    public StoredProcDbContext _context;
    public IConfiguration _configuration {get; }
    public EmployeeController
    (
        StoredProcDbContext context,
        IConfiguration configuration
    )
    {
        _context = context;
        _configuration = configuration;
    }

    // Teha sp andmebaasi, mis annab andmed töötajatest

    public IActionResult Index()
    {
        return View();
    }

    public IEnumerable<Employee> SearchResult()
    {
        var result = _context.Employees
            .FromSqlRaw<Employee>("spSearchEmployees")
            .ToList();

        return result;
    }

}