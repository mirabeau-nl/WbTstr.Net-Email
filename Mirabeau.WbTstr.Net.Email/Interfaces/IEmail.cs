using System.Collections.Generic;

namespace Mirabeau.Wbtstr.Net.Email.Interfaces
{


    public interface IEmail
    {
        string HtmlEmailPath { get; set; }
        List<IAttachment> Attachments { get; set; }
        
    }

    public interface IAttachment
    {
        string Extension { get; set; }
        string FilePath { get; set; }
    }
}
