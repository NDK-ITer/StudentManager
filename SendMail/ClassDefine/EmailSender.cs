using SendMail.Interfaces;
using System.Net.Mail;

namespace SendMail.ClassDefine
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string emailNeedSend, string subject, string message)
        {
            MailMessage myMail = new MailMessage();
            myMail.From = new MailAddress("learntoteach2023@gmail.com");
            myMail.To.Add(emailNeedSend);
            myMail.Subject = subject;
            myMail.IsBodyHtml = true;
            myMail.Body = message;

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(myMail.From.ToString(), "nfqzkctsofxzadxg");
                client.EnableSsl = true;
                client.Send(myMail);
            }

            return Task.CompletedTask;
        }
    }
}
