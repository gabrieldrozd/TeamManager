using System;
using System.Linq;
using System.Windows;
using TeamManager.Database;
using TeamManager.Database.Models;

namespace TeamManager.Views.TM_WindowPages.PagesExtensions
{
    /// <summary>
    /// Interaction logic for UpdateSelectedItemWindow.xaml
    /// </summary>
    public partial class UpdateViewSelectedItemWindow : Window
    {
        Employee employee;
        EmployeeContact employeeContact;

        public UpdateViewSelectedItemWindow(Employee selectedEmployee)
        {
            InitializeComponent();

            this.employee = selectedEmployee;
            this.employeeContact = GetEmployeeContact();

            FillBoxes();
        }

        void FillBoxes()
        {
            firstName.Text = employee.FirstName;
            lastName.Text = employee.LastName;
            position.Text = employee.Position;
            salary.Text = employee.Salary.ToString();
            age.Text = employee.Age.ToString();

            email.Text = employeeContact.EmailAddress;
            phone.Text = employeeContact.PhoneNumber;
            postal.Text = employeeContact.PostalCode;
            city.Text = employeeContact.City;
            street.Text = employeeContact.Street;
            number.Text = employeeContact.HouseNumber;
        }

        EmployeeContact GetEmployeeContact()
        {
            using (var db = new TeamManagerContext())
            {
                var empContact = db.EmployeeContact.Where(x => x.EmpContactId == employee.EmpContactId).SingleOrDefault();

                return empContact;
            }
        }

        bool CanBeUpdated()
        {
            var updatePossible = firstName.Text.Length > 0 &&
                lastName.Text.Length > 0 && position.Text.Length > 0 &&
                salary.Text.Length > 0 && age.Text.Length > 0 &&
                email.Text.Length > 0 && phone.Text.Length > 0 &&
                postal.Text.Length > 0 && city.Text.Length > 0 &&
                street.Text.Length > 0 && number.Text.Length > 0;

            return updatePossible;
        }

        bool IsEmailCorrect()
        {
            var isCorrect = email.Text.Contains("@gmail.com");
            return isCorrect;
        }

        bool IsSalaryNumeric()
        {
            if (!CanBeUpdated())
            {
                return false;
            }
            else
            {
                int numberOfChar = salary.Text.Count();
                if (numberOfChar > 0)
                {
                    bool r = salary.Text.All(char.IsDigit);
                    return r;
                }
                else
                {
                    return false;
                }
            }
        }

        bool IsAgeNumeric()
        {
            if (!CanBeUpdated())
            {
                return false;
            }
            else
            {
                int numberOfChar = age.Text.Count();
                if (numberOfChar > 0)
                {
                    bool r = age.Text.All(char.IsDigit);
                    return r;
                }
                else
                {
                    return false;
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (CanBeUpdated() && IsEmailCorrect() && IsSalaryNumeric() && IsAgeNumeric())
            {
                employee.FirstName = firstName.Text;
                employee.LastName = lastName.Text;
                employee.Position = position.Text;
                employee.Salary = Int32.Parse(salary.Text);
                employee.Age = Int32.Parse(age.Text);

                employeeContact.EmailAddress = email.Text;
                employeeContact.PhoneNumber = phone.Text;
                employeeContact.PostalCode = postal.Text;
                employeeContact.City = city.Text;
                employeeContact.Street = street.Text;
                employeeContact.HouseNumber = number.Text;

                using (var db = new TeamManagerContext())
                {
                    db.Employee.Update(employee);
                    db.EmployeeContact.Update(employeeContact);

                    db.SaveChanges();

                    MessageBox.Show("Updated successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            else if (CanBeUpdated() && IsEmailCorrect() && !IsSalaryNumeric() && IsAgeNumeric())
            {
                MessageBox.Show("Salary must be numeric!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (CanBeUpdated() && IsEmailCorrect() && IsSalaryNumeric() && !IsAgeNumeric())
            {
                MessageBox.Show("Age must be numeric!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (CanBeUpdated() && IsEmailCorrect() && !IsSalaryNumeric() && !IsAgeNumeric())
            {
                MessageBox.Show("Salary and Age must be numeric!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (!IsEmailCorrect())
            {
                MessageBox.Show("Employee e-mail address must be at gmail.com!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                MessageBox.Show("Empty fields!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
