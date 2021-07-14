using System.Windows;
using TeamManager.Database.Helpers;
using TeamManager.Database.Models;

namespace TeamManager.Views.TM_WindowPages.PagesExtensions
{
    /// <summary>
    /// Interaction logic for SendEmailWindow.xaml
    /// </summary>
    public partial class SendEmailWindow : Window
    {
        EmployeeContact employeeContact;

        public SendEmailWindow(EmployeeContact contact)
        {
            InitializeComponent();

            this.employeeContact = contact;

            SetEmailAddress();
        }

        void SetEmailAddress()
        {
            emailTo.IsEnabled = false;

            emailTo.Text = employeeContact.EmailAddress;
        }

        bool IsNotEmpty()
        {
            var isNotEmpty = emailSubject.Text.Length > 0 && emailMessage.Text.Length > 0;
            return isNotEmpty;
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            if (IsNotEmpty())
            {
                EmailSender.SendEmail(employeeContact.EmailAddress, emailSubject.Text, emailMessage.Text);

                MessageBox.Show("Sent!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Empty fields!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
