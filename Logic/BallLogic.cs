using Data;
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

        public void Move(float howFast = 1f)
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
    }
}
