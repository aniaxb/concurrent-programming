using ExampleConsoleApp;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldReturnCorrectSum()
        {
            int a = 2;
            int b = 4;

            int result = Calculator.Sum(a, b);

            Assert.Equal(6, result);
        }
    }
}