using System;

namespace Data
{
    public abstract class BallApi
    {
        public static BallApi CreateBall(int ID, double XPosition, double YPosition, double XDirection, double YDirection, string color)
        {
            return new Ball(ID, XPosition, YPosition, XDirection, YDirection, color);
        }

        public int BallId { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double XDirection { get; set; }
        public double YDirection { get; set; }
        public string Color { get; set; }

        private class Ball : BallApi
        {
            public Ball(int ballId, double xPosition, double yPosition, double xDirection, double yDirection, string color)
            {
                BallId = ballId;
                XPosition = xPosition;
                YPosition = yPosition;
                XDirection = xDirection;
                YDirection = yDirection;
                Color = color;
            }
        }
    }
}
