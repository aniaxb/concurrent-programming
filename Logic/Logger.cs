using Data;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class Logger
{
    private static readonly object _lock = new object();
    private readonly string _filePath;

    public Logger(string filePath)
    {
        _filePath = filePath;
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
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log to a different place or notify the user)
                    Console.WriteLine(ex.Message);
                }
            }
        });
    }
}
