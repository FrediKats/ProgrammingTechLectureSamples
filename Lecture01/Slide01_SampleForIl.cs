namespace Lecture01;

using static System.Console;
using static System.Linq.Enumerable;

public class Slide01_SampleForIl
{
    public static void Show()
    {
        int Square(int x) => x * x;

        int SumOfSquares(int n) =>
            Range(1, n)
                .Select(Square)
                .Sum();

        WriteLine(SumOfSquares(100));
    }
}