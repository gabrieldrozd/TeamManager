using System.ComponentModel.DataAnnotations;

namespace TeamManager.Database.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public int Age { get; set; }

        // Department
        public int DepartmentId { get; set; }

        // Contact
        public int EmpContactId { get; set; }
        public virtual EmployeeContact EmployeeContact { get; set; }
    }
}
