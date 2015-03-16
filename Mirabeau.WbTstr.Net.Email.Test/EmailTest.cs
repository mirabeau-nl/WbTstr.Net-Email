using System.IO;
using Mirabeau.Wbtstr.Net.Email;
using NUnit.Framework;

namespace Mirabeau.WbTstr.Net.Email.Test
{
    [TestFixture]
    class EmailTest
    {

        [Test]
        public void TestEmail()
        {
            const string username = "wocautomation@gmail.com";
            const string password = "testen01";

            var html = "";
            html += "<!DOCTYPE html>";
            html += "<html>";
            html += "<body>";
            html += "<h1>Mirabeau</h1>";
            html += "<p>Test the Test</p>";
            html += "</body>";
            html += "</html>";

            var emailConfig = new EmailConfig
            {
                UserName = username,
                Password = password,
                HostName = "pop.gmail.com",
                Port = 995,
                UseSsl = true
            };

            var emailTool = new EmailTool(emailConfig);
            
            var ms = new MailSender();
            ms.SendEmail(emailTool.UniqueEmailAdress, username, password, html);

            var emailPath = emailTool.GetMail().HtmlEmailPath;
            
            Assert.True(File.Exists(emailPath));
            string emailText = File.ReadAllText(emailPath);
            Assert.True(emailText.Contains("Mirabeau"),"HTML file does not contains \"Mirabeau\"");

        }
    }
}