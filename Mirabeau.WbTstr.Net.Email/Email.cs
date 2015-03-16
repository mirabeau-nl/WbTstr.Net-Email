using System.Collections.Generic;
using Mirabeau.Wbtstr.Net.Email.Interfaces;

namespace Mirabeau.Wbtstr.Net.Email
{
    class Email : IEmail
    {
        public string HtmlEmailPath { get; set; }
        public List<IAttachment> Attachments { get; set; }
    }

    class Attachment : IAttachment
    {
        public string Extension { get; set; }
        public string FilePath { get; set; }
    }
}
