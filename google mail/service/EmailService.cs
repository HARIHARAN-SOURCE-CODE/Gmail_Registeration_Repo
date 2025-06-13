using MailKit;
using MimeKit;
using MailKit.Net;
using System.Threading.Tasks;
namespace google_mail.service
{
    public class EmailService
    {

        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        { 
        _configuration = configuration;
        
        }
        public async Task send( string ToEmail,string Subject, string Body)
        {
            // create head
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("MAPMA", _configuration["EmailSettings:SMTPUsername"]));
            email.To.Add(new MailboxAddress("",ToEmail));
            email.Subject=Subject;
            //body
            var Bbuilder = new BodyBuilder { HtmlBody=Body };
            email.Body = Bbuilder.ToMessageBody();
            // send
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            //connection
            await smtp.ConnectAsync(
                _configuration["EmailSettings:SMTPHost"],
                int.Parse(_configuration["EmailSettings:SMTPPort"]),
               MailKit.Security.SecureSocketOptions.StartTls
                );
            //authentication
            await smtp.AuthenticateAsync(
                _configuration["EmailSettings:SMTPUsername"],
                _configuration["EmailSettings:SMTPPassword"]
                );

            //finalsendcode
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        
        
        }
    }
}
