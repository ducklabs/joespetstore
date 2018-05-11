using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace JoesPetStore.Mail
{
    public class DefaultEmailService : IEmailServer
    {
        public void SendEmail(string to, string message)
        {

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            //If you need to authenticate
            client.Credentials = new NetworkCredential("ian@ducktyping.biz", "password");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("ian@ducklabs.ca");
            mailMessage.To.Add("someone.else@somewhere-else.com");
            mailMessage.Subject = "Hello There";
            mailMessage.Body = "Hello my friend!";

            client.Send(mailMessage);

        }
    }
}