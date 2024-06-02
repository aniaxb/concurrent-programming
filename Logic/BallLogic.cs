using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class BallLogic : INotifyPropertyChanged
    {
        private BallApi ball;
        
        public BallLogic(BallApi ball)
        {
            this.ball = ball;
        }

        public BallApi Ball
        {
            get => ball;
            set
            {
                ball = value;
                RaisePropertyChanged();
            }
        }

        public double XPosition
        {
            get => Ball.XPosition;
            set
            {
                Ball.XPosition = value;
                RaisePropertyChanged();
            }
        }

        public double BallId
        {
            get => Ball.BallId;
            
        }

        public double YPosition
        {
            get => Ball.YPosition;
            set
            {
                Ball.YPosition = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool PositionInBoxX(double x, double xBorder)
        {
            return x < xBorder && x > 0;
        }

        public bool PositionInBoxY(double y, double yBorder)
        {
            return y < yBorder && y > 0;
        }

        public void MoveTest(float howFast = 1f)
        {
            if (!PositionInBoxX(XPosition, 300))
            {
                Ball.XDirection *= -1;
            }

            if (!PositionInBoxY(YPosition, 300))
            {
                Ball.YDirection *= -1;
            }

            Ball.XPosition += howFast * Ball.XDirection;
            Ball.YPosition += howFast * Ball.YDirection;

            RaisePropertyChanged(nameof(XPosition));
            RaisePropertyChanged(nameof(YPosition));
        }

        public void Move(List<BallLogic> allBalls, float howFast = 2f)
        {
            lock (this)
            {
                if (!PositionInBoxX(XPosition, 460 - Ball.Diameter))
                {
                    Ball.XDirection *= -1;
                }

                if (!PositionInBoxY(YPosition, 460 - Ball.Diameter))
                {
                    Ball.YDirection *= -1;
                }

                // Sprawdzanie kolizji z innymi kulami
                foreach (BallLogic otherBall in allBalls)
                {
                    if (otherBall != this && CheckCollision(Ball, otherBall.Ball))
                    {
                        HandleCollision(Ball, otherBall.Ball);
                    }
                }

                Ball.XPosition += howFast * Ball.XDirection;
                Ball.YPosition += howFast * Ball.YDirection;

                RaisePropertyChanged(nameof(XPosition));
                RaisePropertyChanged(nameof(YPosition));

                //logger.Log(ball);
            }
        }

        public bool CheckCollision(BallApi ball1, BallApi ball2)
        {
            double distanceSquared = Math.Pow(ball1.XPosition - ball2.XPosition, 2) + Math.Pow(ball1.YPosition - ball2.YPosition, 2);
            double radiusSumSquared = Math.Pow(ball1.Diameter / 2 + ball2.Diameter / 2, 2);
            return distanceSquared < radiusSumSquared;
        }

        public void HandleCollision(BallApi ball1, BallApi ball2)
        {
            double normalX = ball2.XPosition - ball1.XPosition;
            double normalY = ball2.YPosition - ball1.YPosition;
            double length = Math.Sqrt(normalX * normalX + normalY * normalY);
            normalX /= length;
            normalY /= length;

            double velAlongNormal = (ball2.XDirection - ball1.XDirection) * normalX + (ball2.YDirection - ball1.YDirection) * normalY;

            if (velAlongNormal > 0)
            {
                return;
            }

            double e = 1; // Wspólczynnik restytucji (1 - odbicie idealne)

            double j = -(1 + e) * velAlongNormal;
            j /= 1 / ball1.Mass + 1 / ball2.Mass;

            double impulseX = j * normalX;
            double impulseY = j * normalY;

            ball1.XDirection -= 1 / ball1.Mass * impulseX;
            ball1.YDirection -= 1 / ball1.Mass * impulseY;
            ball2.XDirection += 1 / ball2.Mass * impulseX;
            ball2.YDirection += 1 / ball2.Mass * impulseY;
        }
    }
}
