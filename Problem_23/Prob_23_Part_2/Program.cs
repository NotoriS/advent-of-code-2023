using System.Diagnostics;

public class Problem
{
    public static void Main(string[] args)
    {
        Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

        StreamReader reader = new StreamReader("../../../input.txt");
        List<string> input = new List<string>();

        while (reader.Peek() != -1)
        {
            input.Add(reader.ReadLine());
        }

        Map map = new Map(input);
        Console.WriteLine(map.FindLongestPath());

        watch.Stop();
        Console.WriteLine("Runtime: " + watch.Elapsed);

        Console.ReadKey();
    }
}
