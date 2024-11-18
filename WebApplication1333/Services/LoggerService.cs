
public class LoggerService
{
    private readonly ILogger<LoggerService> _logger;

    public LoggerService(ILogger<LoggerService> logger)
    {
        _logger = logger;
    }

    public void LogMessage(string message)
    {
        _logger.LogInformation("User: {userName} Log message: {Message} at {Time}", message, DateTime.UtcNow);
    }
}
