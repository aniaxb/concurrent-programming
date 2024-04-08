using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;
using Logic;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        bool flag = false;
        public MainViewModel()
        {
            UpdateBallsCommand = new RelayCommand(UpdateBalls);
            StartMovingCommand = new RelayCommand(StartMoving, CanStartMoving);
            StopMovingCommand = new RelayCommand(StopMoving, CanStopMoving);
        }

        public ObservableCollection<BallLogic> Balls { get; } = new ObservableCollection<BallLogic>();

        public ICommand UpdateBallsCommand { get; }
        public ICommand StartMovingCommand { get; }
        public ICommand StopMovingCommand { get; }
        public int NumberOfBalls { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
                BallLogic ballLogic = new BallLogic(ball);
                Balls.Add(ballLogic);
            }
        }

        private bool CanStartMoving() => true; // Warunki potrzebne do uruchomienia animacji
        private void StartMoving()
        {
            flag = true;
            Task.Run(() =>
            {
                while (flag){
                    foreach (BallLogic ball in Balls)
                    {
                        ball.Move();
                    }
                    Thread.Sleep(16);
                }
            });
        }

        private bool CanStopMoving() => true; // Warunki potrzebne do zatrzymania animacji
        private void StopMoving()
        {
            flag = false;
        }
    }
}
