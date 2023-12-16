using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<List<char>> inputGrid = new List<List<char>>();

        while (reader.Peek() != -1)
        {
            inputGrid.Add(reader.ReadLine().ToCharArray().ToList());
        }

        Grid grid = new Grid(inputGrid);
        grid.CastBeam();

        Console.WriteLine(grid.GetEnergizedTileCount());
    }
}