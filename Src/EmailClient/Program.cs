using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Net.Mail;

//required using Microsoft.Extensions.Hosting;
var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services => services.AddScoped<IEmailSender, EmailSender>());

var host = builder.Build();

//required using Microsoft.Extensions.DependencyInjection;
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var emailSender = services.GetRequiredService<IEmailSender>();

        var messageBody = "This is a test email message sent by an application. ";

        string someArrows = new(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
        messageBody += Environment.NewLine + someArrows;

        await emailSender.SendEmailAsync("[RECIPIENT EMAIL ADDRESS]", "[COPIED RECIPIENT EMAIL ADDRESS]", new MailAddress("[SENDER EMAIL ADDRESS]", "Joseph " + (char)0xD8 + " Wambura", System.Text.Encoding.UTF8), "Test", messageBody, "./pdf-test.pdf", encoding: System.Text.Encoding.UTF8);

        await emailSender.SendEmailAsync("[RECIPIENT EMAIL ADDRESS]", "[COPIED RECIPIENT EMAIL ADDRESS]", new MailAddress("[SENDER EMAIL ADDRESS]", "Joseph " + (char)0xD8 + " Wambura", System.Text.Encoding.UTF8), "Test", "<h1>Hello</h1>", "./pdf-test.pdf", true);
    }
    catch (Exception ex)
    {
        //required using Microsoft.Extensions.Logging;
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while sending the email.");
    }
}

host.Run();