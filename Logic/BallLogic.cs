using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class BallLogic : INotifyPropertyChanged
    {
        private Ball ball;

        public BallLogic(Ball ball)
        {
            this.ball = ball;
        }

        public Ball Ball
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

        public string Color
        {
            get => Ball.color;
            set
            {
                Ball.color = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool PositionInBoxX(double x, double y, double xBorder, double yBorder)
        {
            return x < xBorder && x > 0;
        }

        public bool PositionInBoxY(double x, double y, double xBorder, double yBorder)
        {
            return y < yBorder && y > 0;
        }

        public void Move(float howFast = 1f)
        {
            if (!PositionInBoxX(XPosition, YPosition, 300, 300))
            {
                Ball.XDirection *= -1;
            }

            if (!PositionInBoxY(XPosition, YPosition, 300, 300))
            {
                Ball.YDirection *= -1;
            }

            Ball.XPosition += howFast * Ball.XDirection;
            Ball.YPosition += howFast * Ball.YDirection;

            RaisePropertyChanged(nameof(XPosition));
            RaisePropertyChanged(nameof(YPosition));
        }
    }
}
