using System.IO;
using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<List<char>> input = new List<List<char>>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            input.Add(line.ToCharArray().ToList());
        }

        Space space = new Space(input);

        Console.WriteLine(space.GetSumOfShortestPathBetweenEachGalaxy());
    }
}