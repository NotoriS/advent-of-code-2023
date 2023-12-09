using System.IO;
using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<List<List<int>>> fullInput = new List<List<List<int>>>();

        while (reader.Peek() != -1)
        {
            List<int> line = reader.ReadLine().Split(' ').Select(int.Parse).ToList();
            List<List<int>> sequence = new List<List<int>>();
            sequence.Add(line);
            fullInput.Add(sequence);
        }

        reader.Close();

        long sum = 0;

        foreach (List<List<int>> sequence in fullInput)
        {
            for (int i = 0; i < sequence.Count && !ListIsAllZeros(sequence[i]); i++)
            {
                List<int> nextLineInSequence = new List<int>();
                for (int j = 1; j < sequence[i].Count; j++)
                {
                    nextLineInSequence.Add(sequence[i][j] - sequence[i][j - 1]);
                }
                sequence.Add(nextLineInSequence);
            }

            sequence.Last().Insert(0, 0);

            for (int i = sequence.Count - 2; i >= 0; i--)
            {
                sequence[i].Insert(0, sequence[i][0] - sequence[i + 1][0]);
            }

            sum += sequence[0][0];
        }

        Console.WriteLine(sum);
    }

    private static bool ListIsAllZeros(List<int> list)
    {
        foreach (int i in list)
        {
            if (i != 0) return false;
        }
        return true;
    }
}