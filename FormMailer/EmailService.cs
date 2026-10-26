using System.Net;
using System.Net.Mail;

namespace FormMailer;

public class EmailService(IConfiguration config)
{

  public async Task SendEmailAsync(ContactForm form)
  {
    var smtpHost = config["Smtp:Host"];
    var smtpPort = int.Parse(config["Smtp:Port"] ?? "587");
    var smtpUser = config["Smtp:Username"];
    var smtpPass = config["Smtp:Password"];
    var toEmail = config["Smtp:ToEmail"];

    using var client = new SmtpClient(smtpHost, smtpPort)
    {
      Credentials = new NetworkCredential(smtpUser, smtpPass),
      EnableSsl = true
    };

    var mailMessage = new MailMessage
    {
      From = new MailAddress(smtpUser!),
      Subject = $"New Contact Form Submission from {form.Name}",
      Body = $"Name: {form.Name}\nEmail: {form.Email}\n\nMessage:\n{form.Message}",
      IsBodyHtml = false
    };

    mailMessage.To.Add(toEmail!);

    await client.SendMailAsync(mailMessage);
  }
}
