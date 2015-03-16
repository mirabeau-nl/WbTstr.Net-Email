using System.Net.Mail;


namespace Mirabeau.WbTstr.Net.Email.Test
{
    public class MailSender
    {

        public void SendEmail(string recepient, string username, string password, string body)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            var recepientAddress = new MailAddress(recepient);
            var message = new MailMessage();
            message.To.Add(recepientAddress);
            message.From = new MailAddress("test@mirabeau.nl");
            message.IsBodyHtml = true;
            message.Subject = "Test Subject";
            message.Body = body;
            client.Credentials = new System.Net.NetworkCredential(username, password);
            client.EnableSsl = true;
            client.Send(message);

        }

    }
}
