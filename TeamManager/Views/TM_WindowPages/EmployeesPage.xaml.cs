using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TeamManager.Database;
using TeamManager.Database.Helpers;
using TeamManager.Database.Models;
using TeamManager.Views.TM_WindowPages.PagesExtensions;

namespace TeamManager.Views.TM_WindowPages
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        Employee employee;

        public EmployeesPage()
        {
            InitializeComponent();
            FillEmployeesListView();
            DisableButtons();
        }

        void FillEmployeesListView()
        {
            using (var db = new TeamManagerContext())
            {
                employeesListView.ItemsSource = db.Employee.Select(x => x)
                    .Where(x => x.DepartmentId == Informations.CurrentDepartmentId).ToList();
            }
        }

        void DisableButtons()
        {
            UpdateView.IsEnabled = false;
            Delete.IsEnabled = false;
            SendEmail.IsEnabled = false;
        }

        bool HasEmployeesListValues()
        {
            using (var db = new TeamManagerContext())
            {
                var employeesList = db.Employee.Select(x => x)
                    .Where(x => x.DepartmentId == Informations.CurrentDepartmentId).ToList();

                if (employeesList != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        EmployeeContact GetEmployeeContact()
        {
            using (var db = new TeamManagerContext())
            {
                var empContact = db.EmployeeContact.Where(x => x.EmpContactId == employee.EmpContactId).SingleOrDefault();

                return empContact;
            }
        }

        private void employeesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateView.IsEnabled = true;
            Delete.IsEnabled = true;
            SendEmail.IsEnabled = true;

            employee = (Employee)employeesListView.SelectedItem;
        }

        private void UpdateView_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = (Employee)employeesListView.SelectedItem;

            if (selectedEmployee != null)
            {
                var updateViewWindow = new UpdateViewSelectedItemWindow(selectedEmployee);
                updateViewWindow.ShowDialog();
                
                FillEmployeesListView();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            var selectedEmployee = (Employee)employeesListView.SelectedItem;
            var employeeContact = GetEmployeeContact();

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                using (var db = new TeamManagerContext())
                {
                    db.Employee.Remove(selectedEmployee);
                    db.EmployeeContact.Remove(employeeContact);
                    db.SaveChanges();

                    if (HasEmployeesListValues())
                    {
                        FillEmployeesListView();
                    }
                    else
                    {
                        employeesListView.ItemsSource = null;
                        employeesListView.Items.Clear();
                        DisableButtons();
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployeeContact = GetEmployeeContact();

            if (selectedEmployeeContact != null)
            {
                var sendEmailWindow = new SendEmailWindow(selectedEmployeeContact);
                sendEmailWindow.ShowDialog();
            }
        }

        private void SearchEmployees_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchBox = sender as TextBox;

            using (var db = new TeamManagerContext())
            {
                var filteredTasks = db.Employee.Select(x => x).Where(
                    x => (x.FirstName.Contains(searchBox.Text) || x.LastName.Contains(searchBox.Text)) &&
                    x.DepartmentId == Informations.CurrentDepartmentId).ToList();

                employeesListView.ItemsSource = filteredTasks;
            }
        }
    }
}
