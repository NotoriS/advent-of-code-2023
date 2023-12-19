using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        Queue<Instruction> instructions = new Queue<Instruction>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            instructions.Enqueue(new Instruction(line.Split(' ')[0], int.Parse(line.Split(' ')[1])));
        }

        List<Tuple<Tuple<int, int>, Tuple<int, int>>> edges = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();
        foreach (Instruction instruction in instructions)
        {
            instruction.Execute(edges);
        }

        Console.WriteLine(Shoelace(edges));
    }

    private static long Shoelace(List<Tuple<Tuple<int, int>, Tuple<int, int>>> edges)
    {
        long sum = 0;
        foreach (Tuple<Tuple<int, int>, Tuple<int, int>> edge in edges)
        {
            long prod1 = edge.Item1.Item2 * edge.Item2.Item1;
            long prod2 = edge.Item2.Item2 * edge.Item1.Item1;
            sum += prod1 - prod2;
        }
        return sum / 2;
    }
}
