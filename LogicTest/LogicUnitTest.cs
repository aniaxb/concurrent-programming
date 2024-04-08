using Data;
using Logic;

namespace LogicTest
{
    public class LogicTests
    {
        private BallLogic ballLogic;
        private Ball ball;

        [SetUp]
        public void Setup()
        {
            ball = new Ball();
            ballLogic = new BallLogic(ball);
        }

        [Test]
        public void BallLogic_Test_ShouldChangePosition()
        {
            ball.XPosition = 150;
            ball.YPosition = 150;
            ball.XDirection = 1;
            ball.YDirection = 1;

            ballLogic.Move();

            Assert.That(ball.XPosition, Is.EqualTo(151), "XPosition should be incremented by XDirection");
            Assert.That(ball.YPosition, Is.EqualTo(151), "YPosition should be incremented by YDirection");
        }
    }
}