using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TeamManager.Database;
using TeamManager.Database.Helpers;

namespace TeamManager.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DisableComponents();
            FillComboBox();
        }

        // STARTING METHODS
        void DisableComponents()
        {
            loginUsernameTextBox.IsEnabled = false;
            loginPasswordPasswordBox.IsEnabled = false;
            LogInButton.IsEnabled = false;
        }

        void FillComboBox()
        {
            var departments = Informations.GetDepartments();
            departmentsComboBox.ItemsSource = departments;
        }
        // END OF STARTING METHODS

        // CHECKERS
        bool IsUsertInDepartment()
        {
            var userInDepartment = Informations.GetUserId(loginUsernameTextBox.Text) == Informations.CurrentDepartmentId;

            if (userInDepartment)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool IsPasswordCorrect()
        {
            using (var db = new TeamManagerContext())
            {
                var userDbPassword = db.User.Single(x => x.Username == loginUsernameTextBox.Text).Password.ToString();

                if (userDbPassword == loginPasswordPasswordBox.Password.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        bool UsernameHasValue()
        {
            var hasValue = loginUsernameTextBox.Text.Length >= 1;

            return hasValue;
        }

        bool PasswordHasValue()
        {
            var hasValue = loginPasswordPasswordBox.Password.ToString().Length >= 1;

            return hasValue;
        }
        // END OF CHECKERS

        // COMPONENTS EVENTS
        private void departmentsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboSelectedDepartment = departmentsComboBox.SelectedItem.ToString();

            Informations.SetDepartmentInfo(comboSelectedDepartment);

            loginUsernameTextBox.IsEnabled = true;
        }

        private void loginUsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UsernameHasValue() && !PasswordHasValue())
            {
                loginPasswordPasswordBox.IsEnabled = true;
            }
            else if (!UsernameHasValue() && PasswordHasValue())
            {
                loginPasswordPasswordBox.IsEnabled = false;
                loginPasswordPasswordBox.Password = null;
            }
            else
            {
                loginPasswordPasswordBox.IsEnabled = false;
                loginPasswordPasswordBox.Password = null;
            }
        }

        private void loginPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordHasValue())
            {
                LogInButton.IsEnabled = true;
            }
            else if (!PasswordHasValue())
            {
                LogInButton.IsEnabled = false;
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsUsertInDepartment() && IsPasswordCorrect())
            {
                MessageBox.Show("Log in succesful!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                Informations.SetUserInfo(loginUsernameTextBox.Text);
                var mainWindow = new TeamManagerWindow();
                mainWindow.Show();
                this.Close();
            }
            else if (!IsUsertInDepartment())
            {
                MessageBox.Show("There is no such User in this Department!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (IsUsertInDepartment() && !IsPasswordCorrect())
            {
                MessageBox.Show("Wrong password!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
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
        // END COMPONENTS EVENTS
    }
}
