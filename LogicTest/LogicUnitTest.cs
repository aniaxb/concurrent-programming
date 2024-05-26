using NUnit.Framework;
using Logic;
using Data;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Moq;


namespace LogicTest
{
    public class LogicTests
    {
        private BallLogic ballLogic;
        private BallApi ball;
        private Logger logger;

        [SetUp]
        public void Setup()
        {
            ball = BallApi.CreateBall(1, 150, 150, 1, 1, "#FF0000", 10, 20);
            logger = new Logger("test_log.json");
            ballLogic = new BallLogic(ball, logger);
        }

        [Test]
        public void MoveTest_ShouldChangePosition()
        {
            ballLogic.MoveTest();

            Assert.That(ball.XPosition, Is.EqualTo(151), "XPosition should be incremented by XDirection");
            Assert.That(ball.YPosition, Is.EqualTo(151), "YPosition should be incremented by YDirection");
        }

        [Test]
        public void Move_ShouldHandleBorderCollision()
        {
            ball.XPosition = 459;
            ball.XDirection = 1;

            ballLogic.Move(new List<BallLogic>(), 1);

            Assert.That(-1, Is.EqualTo(ball.XDirection), "XDirection should be reversed when hitting right border");
        }

        [Test]
        public void CheckCollision_ShouldReturnTrue_WhenBallsCollide()
        {
            BallApi otherBall = BallApi.CreateBall(2, 158, 158, 1, 1, "#FF0000", 10, 20);

            List<BallLogic> balls = new List<BallLogic>();

            balls.Add(ballLogic);
            balls.Add(new BallLogic(otherBall, logger));

            ballLogic.Move(balls);
            balls.Add(new BallLogic(otherBall, logger));

            ballLogic.Move(balls);

            bool collisionDetected = ballLogic.CheckCollision(ball, otherBall);

            Assert.IsTrue(collisionDetected, "Collision between balls should be detected");
        }

        [Test]
        public void HandleCollision_ShouldChangeBallDirections()
        {
            BallApi otherBall = BallApi.CreateBall(2, 160, 160, -1, -1, "#FF0000", 10, 20);
            double initialXDirection = ball.XDirection;
            double initialYDirection = ball.YDirection;

            ballLogic.HandleCollision(ball, otherBall);

            Assert.That(initialXDirection, Is.Not.EqualTo(ball.XDirection), "XDirection of ball1 should change after collision");
            Assert.That(initialXDirection, Is.Not.EqualTo(ball.XDirection), "YDirection of ball1 should change after collision");

        }
        [Test]
        public void Logger_ShouldLogData()
        {
            var mockLogger = new Mock<Logger>("mock_log.json");
            var ball = BallApi.CreateBall(1, 100, 100, 1, 1, "#FF0000", 10, 20);
            var ballLogic = new BallLogic(ball, mockLogger.Object);

            ballLogic.Move(new List<BallLogic>(), 1);

            mockLogger.Verify(logger => logger.Log(It.IsAny<BallApi>()), Times.Once);
        }

        [Test]
        public void Logging_ShouldNotAffectBallMovement()
        {
            var logger = new Logger("test_log.json");
            var ball = BallApi.CreateBall(1, 100, 100, 1, 1, "#FF0000", 10, 20);
            var ballLogic = new BallLogic(ball, logger);

            double initialX = ball.XPosition;
            double initialY = ball.YPosition;

            ballLogic.Move(new List<BallLogic>(), 1);

            Assert.AreNotEqual(initialX, ball.XPosition);
            Assert.AreNotEqual(initialY, ball.YPosition);
        }
    }
}
