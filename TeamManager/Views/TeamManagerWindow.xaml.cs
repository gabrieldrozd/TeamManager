using System.Windows;
using TeamManager.Views.TM_WindowPages;

namespace TeamManager.Views
{
    /// <summary>
    /// Interaction logic for TeamManagerWindow.xaml
    /// </summary>
    public partial class TeamManagerWindow : Window
    {
        

        public TeamManagerWindow()
        {
            InitializeComponent();

            MainWindowFrame.Content = new EmployeesPage();
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new EmployeesPage();
        }

        private void NewEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = new NewEmployeePage(this);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Do you want to logout?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                this.Close();
            }
        }
    }
}
