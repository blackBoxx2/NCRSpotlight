using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
namespace NCRSPOTLIGHT.Utilities
{
    public static class TestEMailer
    {
        public static void Send(string toEmail, string subject, string body)
        {
            try
            {


            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("devin@devsitconsulting.ca", "devin@devsitconsulting.ca"));
            email.To.Add(MailboxAddress.Parse(toEmail));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.resend.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                smtp.Authenticate("resend", "re_U2sfe9Th_NXeYs6dniKGgpHmF6B26X5NT");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            Console.WriteLine("Email sent successfully");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Failed to send email{ex.ToString()}");
            }
        }
    }
}
