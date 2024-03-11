using ExampleConsoleApp;

class Program
{
    static void Main()
    {
        int a = 2;
        int b = 4;

        int result = Calculator.Sum(a, b);

        Console.WriteLine($"Sum of {a} and {b} is: {result}");
    }
}
