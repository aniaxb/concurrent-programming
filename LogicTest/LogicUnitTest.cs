using Data;
using Logic;
using System.Collections.ObjectModel;

namespace LogicTest
{
    public class LogicTests
    {
        private BallLogic ballLogic;
        private BallApi ball;

        [SetUp]
        public void Setup()
        {
            ball = BallApi.CreateBall(1, 150, 150, 1, 1, "#FF0000", 10, 20);
            ballLogic = new BallLogic(ball);
        }

        [Test]
        public void BallLogic_Test_ShouldChangePosition()
        {

            ballLogic.MoveTest();

            Assert.That(ball.XPosition, Is.EqualTo(151), "XPosition should be incremented by XDirection");
            Assert.That(ball.YPosition, Is.EqualTo(151), "YPosition should be incremented by YDirection");
        }
    }
}