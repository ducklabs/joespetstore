using JoesPetStore.Mail;

namespace JoesPetStore.Tests.Functional
{
    public class MockEmailServerService : IEmailServer
    {
        public bool Sent { get; set; }
        public Email Email { get; set; }

        public void SendEmail(string to, string message)
        {
            Sent = true;
            Email = new Email()
            {
                To = to,
                Message = message
            };
        }
    }

    public class Email
    {
        public string To { get; set; }
        public string Message { get; set; }
    }
}