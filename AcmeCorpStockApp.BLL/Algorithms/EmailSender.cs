using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace AcmeCorpStockApp.BLL.Algorithms
{
    class EmailSender
    {
        public bool SendEmail(string email, string subject, string message)
        {
            try
            {
                var mime = new MimeMessage();

                mime.From.Add(new MailboxAddress("AcmeCorpStock", "aliawan0201@gmail.com"));
                mime.To.Add(new MailboxAddress("AcmeCorpStock", email));
                mime.Subject = subject;
                mime.Body = new TextPart("Plain")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("aliawan0201@gmail.com", "vlscvezfokhzkazw");
                    client.Send(mime);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
