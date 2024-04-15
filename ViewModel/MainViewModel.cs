using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;
using Logic;

namespace ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private bool flag = false;
        private CancellationTokenSource cancellationTokenSource;

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

        private ObservableCollection<BallLogic> existingBalls = new ObservableCollection<BallLogic>();

        private void UpdateBalls()
        {
            var random = new Random();
            var existingBallIds = existingBalls.Select(b => b.Ball.BallId).ToList();
            var newBallIds = Enumerable.Range(0, NumberOfBalls).ToList();

            //Delete balls that are not in updated list
            foreach (var existingBallId in existingBallIds)
            {
                if (!newBallIds.Contains(existingBallId))
                {
                    var ballToRemove = existingBalls.FirstOrDefault(b => b.Ball.BallId == existingBallId);
                    if (ballToRemove != null)
                    {
                        Balls.Remove(ballToRemove);
                        existingBalls.Remove(ballToRemove);
                    }
                }
            }

            //Add new balls to the list
            foreach (var newBallId in newBallIds)
            {
                BallLogic ballLogic;
                var existingBall = existingBalls.FirstOrDefault(b => b.Ball.BallId == newBallId);
                if (existingBall != null)
                {
                    ballLogic = existingBall;
                }
                else
                {
                    var ball = BallApi.CreateBall(
                        newBallId, 
                        random.NextDouble() * 300, 
                        random.NextDouble() * 300, 
                        random.NextDouble(), 
                        random.NextDouble(), 
                        $"#{random.Next(0x1000000):X6}");
                    ballLogic = new BallLogic(ball);
                    existingBalls.Add(ballLogic);
                }

                if (!Balls.Contains(ballLogic))
                {
                    Balls.Add(ballLogic);
                }
            }
        }

        private bool CanStartMoving() => true; //Conditions needed to start the animation
        private void StartMoving()
        {
            flag = true;
            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (flag)
                {
                    foreach (BallLogic ball in Balls)
                    {
                        ball.Move();
                    }
                    await Task.Delay(16, cancellationTokenSource.Token);
                }
            }, cancellationTokenSource.Token);
        }

        private bool CanStopMoving() => true; // Conditions needed to stop the animation
        private void StopMoving()
        {
            flag = false;
            cancellationTokenSource?.Cancel();
        }
    }
}
