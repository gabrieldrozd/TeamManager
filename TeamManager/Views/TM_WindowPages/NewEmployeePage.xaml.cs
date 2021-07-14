using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TeamManager.Database;
using TeamManager.Database.Helpers;
using TeamManager.Database.Models;

namespace TeamManager.Views.TM_WindowPages
{
    /// <summary>
    /// Interaction logic for NewEmployeePage.xaml
    /// </summary>
    public partial class NewEmployeePage : Page
    {
        TeamManagerWindow tmWindow;

        public NewEmployeePage(TeamManagerWindow window)
        {
            InitializeComponent();

            this.tmWindow = window;
        }

        bool CanBeAdded()
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
            if (!CanBeAdded())
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
            if (!CanBeAdded())
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

        void ClearText()
        {
            firstName.Text = null;
            lastName.Text = null;
            position.Text = null;
            salary.Text = null;
            age.Text = null;
            
            email.Text = null;
            phone.Text = null;
            postal.Text = null;
            city.Text = null;
            street.Text = null;
            number.Text = null;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CanBeAdded() && IsEmailCorrect() && IsSalaryNumeric() && IsAgeNumeric())
            {
                using (var db = new TeamManagerContext())
                {
                    var employeeTable = db.Employee;
                    var emplContactTable = db.EmployeeContact;

                    var employee = new Employee
                    {
                        FirstName = firstName.Text,
                        LastName = lastName.Text,
                        Position = position.Text,
                        Salary = Int32.Parse(salary.Text),
                        Age = Int32.Parse(age.Text),

                        DepartmentId = Informations.CurrentDepartmentId,
                        EmpContactId = Informations.GetLastEmployeeContactId() + 1
                    };

                    var empContact = new EmployeeContact
                    {
                        EmailAddress = email.Text,
                        PhoneNumber = phone.Text,
                        PostalCode = postal.Text,
                        City = city.Text,
                        Street = street.Text,
                        HouseNumber = number.Text,

                        EmployeeId = Informations.GetLastEmployeeId() + 1
                    };

                    employeeTable.Add(employee);
                    emplContactTable.Add(empContact);

                    db.SaveChanges();

                    MessageBox.Show("Added successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    tmWindow.MainWindowFrame.Content = new EmployeesPage();
                }
            }
            else if (CanBeAdded() && IsEmailCorrect() && !IsSalaryNumeric() && IsAgeNumeric())
            {
                MessageBox.Show("Salary must be numeric!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (CanBeAdded() && IsEmailCorrect() && IsSalaryNumeric() && !IsAgeNumeric())
            {
                MessageBox.Show("Age must be numeric!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (CanBeAdded() && IsEmailCorrect() && !IsSalaryNumeric() && !IsAgeNumeric())
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
        }
    }
}
