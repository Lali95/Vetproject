using Vetproject.Service;

public class LoggerService : ILoggerService
{
    public void LogError(string message)
    {
        Console.WriteLine($"Error: {message}");
    }
}