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
            input.Add(reader.ReadLine().ToCharArray().ToList());
        }

        reader.Close();

        Grid grid = new Grid(input);

        Console.WriteLine(grid.GetNumberOfGroundTilesContainedByLoop());
    }
}
