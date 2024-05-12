
using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Model
    {
        public List<BallApi> Balls { get; set; } = new List<BallApi>();

        public void UpdateBallPosition(int ballId, double xPosition, double yPosition)
        {
            var ballToUpdate = Balls.FirstOrDefault(ball => ball.BallId == ballId);
            if (ballToUpdate != null)
            {
                ballToUpdate.XPosition = xPosition;
                ballToUpdate.YPosition = yPosition;
            }
        }
    }
}

