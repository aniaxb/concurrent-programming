using NUnit.Framework;
using Logic;
using Data;
using System.Collections.Generic;

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
            balls.Add(new BallLogic(otherBall));

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
    }
}
