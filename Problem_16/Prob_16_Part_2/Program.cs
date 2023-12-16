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

        int max = 0;

        for (int i = 1; i <= inputGrid[0].Count; i++)
        {
            // Top row
            Grid grid = new Grid(inputGrid);
            grid.CastBeam(0, i, Vector.down);
            max = Math.Max(max, grid.GetEnergizedTileCount());

            // Bottom row
            grid = new Grid(inputGrid);
            grid.CastBeam(inputGrid.Count, i, Vector.up);
            max = Math.Max(max, grid.GetEnergizedTileCount());
        }

        for (int i = 1; i <= inputGrid.Count; i++)
        {
            // Left column
            Grid grid = new Grid(inputGrid);
            grid.CastBeam(i, 0, Vector.right);
            max = Math.Max(max, grid.GetEnergizedTileCount());

            // Right column
            grid = new Grid(inputGrid);
            grid.CastBeam(i, inputGrid[0].Count, Vector.left);
            max = Math.Max(max, grid.GetEnergizedTileCount());
        }

        Console.WriteLine(max);
    }
}