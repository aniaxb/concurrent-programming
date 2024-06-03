using Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Timers;

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
        _timer = new Timer(2000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = false; 
        Console.WriteLine($"Logger initialized with file path: {_filePath}");
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        lock (_lock)
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
    }

    public void UpdateBalls(List<BallLogic> balls)
    {
        lock (_lock)
        {
            _balls = balls;
        }
    }

    public void StartLogging()
    {
        _timer.Start();
    }

    public void StopLogging()
    {
        _timer.Stop();
    }
}
