using System.Net;
using System.Net.Mail;

namespace FormMailer;

public class EmailService
{
  private readonly IConfiguration _config;

  public EmailService(IConfiguration config)
  {
    _config = config;
  }

  public async Task SendEmailAsync(ContactForm form)
  {
    var smtpHost = _config["Smtp:Host"];
    var smtpPort = int.Parse(_config["Smtp:Port"] ?? "587");
    var smtpUser = _config["Smtp:Username"];
    var smtpPass = _config["Smtp:Password"];
    var toEmail = _config["Smtp:ToEmail"];

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
