using Data;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class Logger
{
    private static readonly object _lock = new object();
    private readonly string _filePath;

    public Logger(string filePath)
    {
        _filePath = filePath;
        Console.WriteLine($"Logger initialized with file path: {_filePath}");
    }

    public void Log(BallApi ball)
    {
        Task.Run(() =>
        {
            lock (_lock)
            {
                try
                {
                    var logEntry = new
                    {
                        BallId = ball.BallId,
                        XPosition = ball.XPosition,
                        YPosition = ball.YPosition,
                        Timestamp = DateTime.Now
                    };

                    string jsonString = JsonSerializer.Serialize(logEntry);
                    File.AppendAllText(_filePath, jsonString + Environment.NewLine);
                    Console.WriteLine($"Logged entry: {jsonString}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to log file: {ex.Message}");
                }
            }
        });
    }
}
