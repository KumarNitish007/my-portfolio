using MailKit.Net.Smtp;
using MimeKit;


namespace portfolioApp.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        // public async Task SendEmailAsync(string to, string subject, string message)
        // {
        //     var emailMessage = new MimeMessage();
        //     emailMessage.From.Add(new MailboxAddress("nknitish043@gmail.com", _smtpUser));
        //     emailMessage.To.Add(new MailboxAddress("", to));
        //     emailMessage.Subject = subject;
        //     emailMessage.Body = new TextPart("Hello Neo html")
        //     {
        //         Text = message
        //     };

        //     using (var client = new SmtpClient())
        //     {
        //         try
        //         {
        //             await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        //             await client.AuthenticateAsync(_smtpUser, _smtpPass);
        //             await client.SendAsync(emailMessage);
        //             await client.DisconnectAsync(true);

        //         }
        //         catch (Exception ex)
        //         {
        //             throw ex;
        //         }
        //     }
        // }

        public async Task SendEmailAsyncportfolio(string to, string subject, string message)
        {
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentException("The recipient email address is required.", nameof(to));
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", _smtpUser)); // Ensure the 'From' email is your SMTP user
            emailMessage.To.Add(new MailboxAddress("", "Nknitish043@gmail.com"));          // The 'To' email comes from the `to` parameter

            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html")                  // Set content type as "html"
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_smtpUser, _smtpPass);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to send email", ex);
                }
            }
        }

    }
}
