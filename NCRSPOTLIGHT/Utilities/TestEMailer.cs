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
            email.From.Add(new MailboxAddress("rojaylarinze@gmail.com", "rojaylarinzegmail.com"));
            email.To.Add(MailboxAddress.Parse(toEmail));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.resend.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                smtp.Authenticate("resend", "re_Xpb1Licc_MratmivBhQMsPm3QPJbFZ7de");
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
