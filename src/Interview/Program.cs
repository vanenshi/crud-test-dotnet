using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public class Program
{
    public static void Main(string[] args)
    {
        int a = 0;
        int b = 1;

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(a);
            var t = b;
            b = a + b;
            a = t;
        }
    }
}
