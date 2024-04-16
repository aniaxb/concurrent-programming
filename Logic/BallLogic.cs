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

        public void Move(List<BallLogic> allBalls, float howFast = 1f)
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
        }

        private bool CheckCollision(BallApi ball1, BallApi ball2)
        {
            double distance = Math.Sqrt(Math.Pow(ball1.XPosition - ball2.XPosition, 2) + Math.Pow(ball1.YPosition - ball2.YPosition, 2));
            return distance < (ball1.Diameter/2 + ball2.Diameter/2);
        }

        private void HandleCollision(BallApi ball1, BallApi ball2)
        {
            // Obliczanie wektora normalnego od punktu kontaktu do �rodka kuli
            double normalX = ball2.XPosition - ball1.XPosition;
            double normalY = ball2.YPosition - ball1.YPosition;
            double length = Math.Sqrt(normalX * normalX + normalY * normalY);
            normalX /= length;
            normalY /= length;

            // Obliczanie sk�adowej pr�dko�ci wzd�u� wektora normalnego
            double velAlongNormal = (ball2.XDirection - ball1.XDirection) * normalX + (ball2.YDirection - ball1.YDirection) * normalY;

            // Je�li sk�adowa jest dodatnia, to kulki si� oddalaj�, nie wykonujemy odbicia
            if (velAlongNormal > 0)
            {
                return;
            }

            // Obliczanie wsp�czynnika restytucji
            double e = 1; // Wsp�czynnik restytucji (1 - odbicie idealne)

            // Obliczanie sk�adowej pr�dko�ci po odbiciu wzd�u� wektora normalnego
            double j = -(1 + e) * velAlongNormal;
            j /= 1 / ball1.Mass + 1 / ball2.Mass;

            // Obliczanie zmiany pr�dko�ci
            double impulseX = j * normalX;
            double impulseY = j * normalY;

            // Aktualizacja pr�dko�ci kul
            ball1.XDirection -= 1 / ball1.Mass * impulseX;
            ball1.YDirection -= 1 / ball1.Mass * impulseY;
            ball2.XDirection += 1 / ball2.Mass * impulseX;
            ball2.YDirection += 1 / ball2.Mass * impulseY;
        }

    }
}
