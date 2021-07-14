using System.Collections.Generic;
using System.Linq;
using TeamManager.Database.Models;

namespace TeamManager.Database.Helpers
{
    public static class Informations
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserUsername { get; set; }
        public static int CurrentDepartmentId { get; set; }
        public static string CurrentDepartmentName { get; set; }
        public static List<string> DepartamentsList { get; set; }

        public static List<string> GetDepartments()
        {
            using (var db = new TeamManagerContext())
            {
                DepartamentsList = db.Department.OrderBy(x => x.DepartmentName)
                    .Select(x => x.DepartmentName).ToList();

                return DepartamentsList;
            }
        }

        public static int GetUserId(string username)
        {
            using (var db = new TeamManagerContext())
            {
                var userId = db.User.Where(x => x.Username == username)
                    .Select(x => x.UserId).FirstOrDefault();

                return userId;
            }
        }

        public static void SetDepartmentInfo(string departmentName)
        {
            using (var db = new TeamManagerContext())
            {
                CurrentDepartmentName = departmentName;
                
                CurrentDepartmentId = db.Department.Where(x => x.DepartmentName == Informations.CurrentDepartmentName)
                .Select(x => x.DepartmentId).FirstOrDefault();
            }
        }

        public static void SetUserInfo(string enteredUsername)
        {
            using (var db = new TeamManagerContext())
            {
                CurrentUserId = db.User.Where(x => x.Username == enteredUsername)
                .Select(x => x.UserId).FirstOrDefault();

                CurrentUserUsername = db.User.Where(x => x.UserId == Informations.CurrentUserId)
                    .Select(x => x.Username).FirstOrDefault();
            }
        }

        public static int GetLastEmployeeId()
        {
            using (var db = new TeamManagerContext())
            {
                var lastEmployeeId = db.Employee.DefaultIfEmpty().Max(x => x == null ? 0 : x.EmployeeId);
                
                return lastEmployeeId;
            }
        }

        public static int GetLastEmployeeContactId()
        {
            using (var db = new TeamManagerContext())
            {
                var lastContactId = db.EmployeeContact.DefaultIfEmpty().Max(x => x == null ? 0 : x.EmpContactId);

                return lastContactId;
            }
        }

        public static void AddUserAndDepartment(string departmentName, string departmentCity, string username, string password)
        {
            using (var db = new TeamManagerContext())
            {
                var departamentTable = db.Department;
                var userTable = db.User;

                var department = new Department
                {
                    DepartmentName = departmentName,
                    DepartmentCity = departmentCity,
                };

                var user = new User
                {
                    Username = username,
                    Password = password,

                    Department = department
                };

                db.Department.Add(department);
                db.User.Add(user);

                db.SaveChanges();
            }
        }

        public static void AddUsrAndDep()
        {
            Informations.AddUserAndDepartment("Rzeszow Department", "Rzeszow", "rzAdmin", "rzAdmin");
            Informations.AddUserAndDepartment("Krakow Department", "Krakow", "krAdmin", "krAdmin");
            Informations.AddUserAndDepartment("Wroclaw Department", "Wroclaw", "wrAdmin", "wrAdmin");
        }
    }
}
