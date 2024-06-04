using Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Xml.Serialization;


public class Logger
{
    private static readonly object _lock = new object();
    private readonly string _filePath;
    private Timer _timer;
    private List<BallLogic> _balls;

    public Logger(string filePath)
    {
        _filePath = filePath;
        _balls = new List<BallLogic>();
        _timer = new Timer(TimerCallback, null, 5, 2000);
        Console.WriteLine($"Logger initialized with file path: {_filePath}");
    }

    private void TimerCallback(Object o)
    {
        if (_balls.Count == 0)
        {
            File.AppendAllText(_filePath, "ball list is empty" + Environment.NewLine);
        }
        else
        {
            foreach (BallLogic ball in _balls)
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


    public void UpdateBalls(List<BallLogic> balls)
    {

        _balls = balls;

    }

}