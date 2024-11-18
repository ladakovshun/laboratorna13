using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Настройка EmailConnectionInfo для отправки логов
var emailConnectionInfo = new EmailConnectionInfo
{
    FromEmail = "abe.frami54@ethereal.email",
    ToEmail = "alford.bayer49@ethereal.email",
    MailServer = "smtp.ethereal.email",
    Port = 587,
    EnableSsl = true,
    NetworkCredentials = new NetworkCredential("abe.frami54@ethereal.email", "Fp1jEb7kvCCAArWXB9"),
    EmailSubject = "Log Notification"
};

// Конфигурация Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/log.txt",
        rollingInterval: RollingInterval.Hour,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.Email(emailConnectionInfo, restrictedToMinimumLevel: LogEventLevel.Verbose)
    .CreateLogger();

builder.Host.UseSerilog();

// Настройка Kestrel на порту 7142
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 7142); // Используем порт 7142
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Тестовая отправка сообщения
void TestEmailLogging()
{
    try
    {
        // Генерация тестового сообщения для уровня Verbose
        Log.Verbose("This is a verbose log message to trigger email notification.");
        Console.WriteLine("Verbose email log has been generated.");
    }
    catch (Exception ex)
    {
        Log.Error($"Failed to generate verbose email log: {ex.Message}");
    }

    // Проверка отправки вручную
    try
    {
        var smtpClient = new SmtpClient("smtp.ethereal.email")
        {
            Port = 587,
            Credentials = new NetworkCredential("abe.frami54@ethereal.email", "Fp1jEb7kvCCAArWXB9"),
            EnableSsl = true
        };

        // Отправка тестового сообщения через SMTP
        smtpClient.Send("abe.frami54@ethereal.email", "alford.bayer49@ethereal.email",
            "Verbose Test Email", "This is a manually sent test email at Verbose level.");
        Console.WriteLine("Manual test email sent successfully.");
    }
    catch (Exception ex)
    {
        Log.Error($"Failed to send manual test email: {ex.Message}");
    }
}

TestEmailLogging();

app.Run();
