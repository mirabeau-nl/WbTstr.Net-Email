using Mirabeau.Wbtstr.Net.Email.Interfaces;

namespace Mirabeau.Wbtstr.Net.Email
{
   public class EmailConfig : IEmailConfig
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
