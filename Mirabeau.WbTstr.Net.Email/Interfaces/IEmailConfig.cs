namespace Mirabeau.Wbtstr.Net.Email.Interfaces
{
    public interface IEmailConfig
    {
        string HostName { get; set; }
        int Port { get; set; }
        bool UseSsl { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
