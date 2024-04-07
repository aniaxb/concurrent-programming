
using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Logic
{
    public class BallLogic
    {
        public BallLogic(ObservableCollection<Ball> balls)
        {
            Balls = balls;
        }

        public ObservableCollection<Ball> Balls { get; set; }
        public List<Ball> workingBalls { get; set; }

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
            workingBalls = new List<Ball>();


            foreach (Ball ball in Balls)
            {
                workingBalls.Add(ball);

            }

            foreach (Ball ball in workingBalls)
            {
                if (!PositionInBoxX(ball.XPosition, ball.YPosition, 300, 300)) //TODO: Replace hardcoded borders
                {
                    ball.XDirection *= -1;
                }

                if (!PositionInBoxY(ball.XPosition, ball.YPosition, 300, 300)) //TODO: Replace hardcoded borders
                {
                    ball.YDirection *= -1;
                }

                ball.XPosition += howFast * ball.XDirection;
                ball.YPosition += howFast * ball.YDirection;
            }
            Balls.Clear();

            foreach (Ball ball in workingBalls)
            {
                Balls.Add(ball);
            }
            
        }

    }
}
