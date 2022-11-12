using System.Net.Mail;

public interface IEmailSender
{
    Task SendEmailAsync(string to, string cc, MailAddress from, string subject, string body, string fileName, bool isBodyHtml = false, System.Text.Encoding encoding = null);
}