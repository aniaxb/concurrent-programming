
using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

namespace Logic
{
    public class BallLogic : INotifyPropertyChanged
    {
        private double x;
        private double y;
        private string color;

        public BallLogic(Ball ball)
        {
            Ball = ball;
            X = ball.XPosition; 
            Y = ball.YPosition;
            color = ball.color;
        }

        public Ball Ball { get; set; }

        public double X { get => x; 
            set 
            {
                x = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public double Y { get => y; 
            set 
            {
                y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }

        public string Color { get => color; 
                       set
                {
                    color = value;
                    RaisePropertyChanged(nameof(Color));
                }
            }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
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
            if(!PositionInBoxX(X, Y, 300, 300))
            {
                Ball.XDirection *= -1;
            }

            if (!PositionInBoxY(X, Y, 300, 300))
            {
                Ball.YDirection *= -1;
            }

            Ball.XPosition += howFast * Ball.XDirection;
            Ball.YPosition += howFast * Ball.YDirection;

            X = Ball.XPosition;
            Y = Ball.YPosition;
        }

    }
}
