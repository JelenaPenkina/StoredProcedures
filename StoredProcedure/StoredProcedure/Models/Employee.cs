using System.ComponentModel.DataAnnotations;
namespace   StoredProcedure.Models;

    public class Employee
    {
        [Key]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Salary { get; set; } = 0;
    }