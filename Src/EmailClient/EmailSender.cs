using System.Net.Mail;
using System.Net;
using System.ComponentModel;

public class EmailSender : IEmailSender
{
    public EmailSender()
    {

    }

    public async Task SendEmailAsync(string to, string cc, MailAddress from, string subject, string body, string fileName, bool isBodyHtml, System.Text.Encoding encoding)
    {
        #region SMTP Mail

        var adminEmailAddress = "email account to use to send";
        var adminAccountPassword = "password for the email above";
        var apiKey = "[SENDGRID API KEY]";

        var emailClient = new SmtpClient()
        {
            EnableSsl = true,
            Port = 587,
            Host = "smtp.gmail.com",
            Credentials = new NetworkCredential(adminEmailAddress, adminAccountPassword),
            //Host = "smtp.zoho.com",
            //Credentials = new NetworkCredential(adminEmailAddress, adminAccountPassword),
            //Host = "smtp.sendgrid.net",
            //Credentials = new NetworkCredential("apikey", apiKey),
        };

        var message = new MailMessage
        {
            From = from,
            Subject = subject,
            Body = body,
            IsBodyHtml = isBodyHtml,
            BodyEncoding = encoding,
            SubjectEncoding = encoding,
        };

        message.To.Add(new MailAddress(to));
        message.CC.Add(new MailAddress(cc));
        message.Attachments.Add(new Attachment(fileName));

        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        var userState = Guid.NewGuid().ToString();
        emailClient.SendAsync(message, userState);

        //await emailClient.SendMailAsync(message, CancellationToken.None);

        // Set the method that is called back when the send operation ends.
        emailClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

        if (!mailSent)
        {
            Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            if ($"{Console.ReadLine()}".StartsWith("c") && mailSent == false)
            {
                emailClient.SendAsyncCancel();
            }
        }

        // Clean up.
        //message.Dispose();

        #endregion
    }

    static bool mailSent = false;

    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        var defaultColor = Console.ForegroundColor;
        // Get the unique identifier for this asynchronous operation.
        String token = (string)e.UserState;

        if (e.Cancelled)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[{0}] Send canceled.", token);
        }
        if (e.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Message sent.");
        }
        Console.ForegroundColor = defaultColor;
        mailSent = true;
    }
}