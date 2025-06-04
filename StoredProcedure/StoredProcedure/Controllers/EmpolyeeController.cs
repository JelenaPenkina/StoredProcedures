using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

    [HttpGet]
    public IActionResult DynamicSql()
    {
        string connectionStr = _configuration.GetConnectionString("DeafaultConnection");

        using (SqlConnection con = new SqlConnection(connectionStr))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "dbo.spSearchEmployees";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            List<Employee> model = new List<Employee>();
            while (sdr.Read())
            {
                var details = new Employee();
                details.FirstName = sdr["FirstName"].ToString();
                details.LastName = sdr["LastName"].ToString();
                details.Gender = sdr["Gender"].ToString();
                details.Salary = Convert.ToInt32(sdr["Salary"]);
                model.Add(details);
            }
            return View(model);

        }

    }

}