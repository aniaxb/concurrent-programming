using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public virtual void Log(List<BallLogic> balls)
    {
        if (balls.Count == 0)
        {
            File.AppendAllText(_filePath, "ball list is empty" + Environment.NewLine);
        }
        else
        {
            foreach (BallLogic ball in balls)
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
        }  
    }
}
