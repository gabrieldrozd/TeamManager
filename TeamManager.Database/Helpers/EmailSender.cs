using System.Net;
using System.Net.Mail;

namespace TeamManager.Database.Helpers
{
    public static class EmailSender
    {
        private static string TeamManagerEmailAddress { get; set; } = "tManagerApp@gmail.com";
        private static string TeamManagerEmailPassword { get; set; } = "tManager1@";
        private static string SmtpProtocol { get; set; } = "smtp.gmail.com";
        private static int Port { get; set; } = 587;



        public static void SendEmail(string employeeEmail, string emailSubject, string emailContent)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(TeamManagerEmailAddress);
                mail.To.Add(employeeEmail);
                mail.Subject = emailSubject;
                mail.Body = emailContent;

                using (SmtpClient smtp = new SmtpClient(SmtpProtocol, Port))
                {
                    smtp.Credentials = new NetworkCredential(TeamManagerEmailAddress, TeamManagerEmailPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
