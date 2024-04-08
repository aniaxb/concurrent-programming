using Data;

namespace DataTest
{
    public class DataTests
    {
        private Ball ball;

        [SetUp]
        public void Setup()
        {
            ball = new Ball();
        }

        [Test]
        public void Ball_Test_GetSetProperties()
        {
            ball.BallId = 1;
            ball.XPosition = 10.5;
            ball.YPosition = 20.7;
            ball.XDirection = 0.8;
            ball.YDirection = -0.6;
            ball.color = "#FF0000";

            Assert.That(ball.BallId, Is.EqualTo(1));
            Assert.That(ball.XPosition, Is.EqualTo(10.5));
            Assert.That(ball.YPosition, Is.EqualTo(20.7));
            Assert.That(ball.XDirection, Is.EqualTo(0.8));
            Assert.That(ball.YDirection, Is.EqualTo(-0.6));
            Assert.That(ball.color, Is.EqualTo("#FF0000"));
        }
    }
}