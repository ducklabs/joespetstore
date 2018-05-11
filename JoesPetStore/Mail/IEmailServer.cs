namespace JoesPetStore.Mail
{
    public interface IEmailServer
    {
        void SendEmail(string to, string message);
    }
}