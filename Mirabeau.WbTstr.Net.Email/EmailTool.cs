using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Mirabeau.Wbtstr.Net.Email.Interfaces;
using OpenPop.Mime;
using OpenPop.Pop3;

namespace Mirabeau.Wbtstr.Net.Email
{
    public class EmailTool
    {
        public string UniqueEmailAdress { get; private set; }

        private readonly IEmailConfig _configuration;

        public EmailTool(IEmailConfig configuration)
        {
            _configuration = configuration;
            UniqueEmailAdress = GenerateUniqueEmailAddress();
        }

            public IEmail GetMail()
            {
                IEmail email = new Mirabeau.Wbtstr.Net.Email.Email();
                var message = FetchMessageByRecepientWithRetry();
                email.HtmlEmailPath = SaveHtml(message);
                email.Attachments = SaveAttachments(message);
                return email;
            }

            private string GenerateUniqueEmailAddress()
            {
                var splitEmail = _configuration.UserName.Split('@');
                var email = splitEmail[0] + "+" + Guid.NewGuid() + "@" + splitEmail[1];
                return email;
            }

            private List<IAttachment> SaveAttachments(Message message)
            {
                var savedAttachments = new List<IAttachment>();
                foreach (MessagePart attachment in message.FindAllAttachments())
                {
                    IAttachment savedAttachment = new Attachment();
                    string fileName = string.Format("attachment_{0}_{1}", Guid.NewGuid(),attachment.FileName);
                    string filePath = Path.Combine(Path.GetTempPath(), fileName);
                    attachment.Save(new FileInfo(filePath));
                    savedAttachment.FilePath = filePath;
                    savedAttachment.Extension = Path.GetExtension(attachment.FileName);
                }
                return savedAttachments;
            }

            private static string SaveHtml(Message message)
            {
                string fileName = string.Format("email_{0}.html", Guid.NewGuid());
                string filePath = Path.Combine(Path.GetTempPath(), fileName);
                MessagePart html = message.FindFirstHtmlVersion();
                html.Save(new FileInfo(filePath));
                return filePath;
            }

            private Message FetchMessageByRecepientWithRetry()
            {
                const int maxTries = 5;
                int i = 0;
                Message foundMessage = null;
                while (i < maxTries && foundMessage == null)
                {
                    Thread.Sleep(3000);
                    foundMessage = FetchMessageByRecepient();
                    i++;
                }

                if (foundMessage == null)
                {
                    throw new Exception("No e-mail found");
                }
                
                return foundMessage;
            }
        
            private Message FetchMessageByRecepient()
            {
                var allMessages = FetchAllMessages();
                DateTime biggestDate = DateTime.MinValue;
                Message matchMessage = null;


                foreach (var m in allMessages)
                {
                    var recepients = m.Headers.To.ToArray();
                    var messageDate = m.Headers.DateSent;
                   
                    foreach (var r in recepients)
                    {
                        var recepient = r.ToString();
                        if (recepient.Contains(UniqueEmailAdress) && biggestDate < messageDate)
                        {
                            biggestDate = messageDate;
                            matchMessage = m;
                        }

                    }

                }
                
                return matchMessage;
            }

            private IEnumerable<Message> FetchAllMessages()
            {

                using (var client = new Pop3Client())
                {
                    client.Connect(_configuration.HostName, _configuration.Port, _configuration.UseSsl);
                    client.Authenticate(_configuration.UserName, _configuration.Password);
                    int messageCount = client.GetMessageCount();
                    var allMessages = new List<Message>(messageCount);

                    for (int i = messageCount; i > 0; i--)
                    {
                        allMessages.Add(client.GetMessage(i));
                    }

                    return allMessages;
                }
            }
        }

   
}
