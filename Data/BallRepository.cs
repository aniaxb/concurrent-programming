using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public interface IBallRepository
    {
        IEnumerable<Ball> GetBalls();
        void AddBall(Ball ball);
        void RemoveBall(Ball ball);
        void ClearBalls();
    }

    public class BallRepository : IBallRepository
    {
        private List<Ball> _balls = new List<Ball>();

        public IEnumerable<Ball> GetBalls()
        {
            return _balls.ToList();
        }

        public void AddBall(Ball ball)
        {
            _balls.Add(ball);
        }

        public void RemoveBall(Ball ball)
        {
            _balls.Remove(ball);
        }

        public void ClearBalls()
        {
            _balls.Clear();
        }
    }
}
