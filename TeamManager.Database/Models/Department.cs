using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamManager.Database.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCity { get; set; }

        // User One-One
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public List<Employee> Employee { get; set; }
    }
}
