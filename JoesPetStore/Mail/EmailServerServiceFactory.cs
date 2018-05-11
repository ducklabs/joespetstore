namespace JoesPetStore.Mail
{
    public class EmailServerServiceFactory
    {
        public static IEmailServer EmailServer;

        public static void UseEmailServer(IEmailServer emailServer)
        {
            EmailServer = emailServer;
        }
    }
}