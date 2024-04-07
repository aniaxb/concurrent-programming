using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;

namespace ViewModel
{
    public class MainViewModel : ObservableRecipient
    {
        public MainViewModel()
        {
            UpdateBallsCommand = new RelayCommand(UpdateBalls);
            StartMovingCommand = new RelayCommand(StartMoving, CanStartMoving);
            StopMovingCommand = new RelayCommand(StopMoving, CanStopMoving);
        }

        public ObservableCollection<Ball> Balls { get; } = new ObservableCollection<Ball>();

        public ICommand UpdateBallsCommand { get; }
        public ICommand StartMovingCommand { get; }
        public ICommand StopMovingCommand { get; }
        public int NumberOfBalls { get; set; }

        private void UpdateBalls()
        {
            Balls.Clear();
            var random = new Random();
            for (int i = 0; i < NumberOfBalls; i++)
            {
                var ball = new Ball
                {
                    BallId = i,
                    XPosition = random.NextDouble() * 300,
                    YPosition = random.NextDouble() * 300,
                    XDirection = random.NextDouble(),
                    YDirection = random.NextDouble(),
                    color = $"#{random.Next(0x1000000):X6}"
                };
                Balls.Add(ball);
            }
        }

        private bool CanStartMoving() => true; // Warunki potrzebne do uruchomienia animacji
        private void StartMoving()
        {
            // Rozpocznij animację
        }

        private bool CanStopMoving() => true; // Warunki potrzebne do zatrzymania animacji
        private void StopMoving()
        {
            // Zatrzymaj animację
        }
    }
}
