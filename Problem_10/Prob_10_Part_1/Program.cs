using System.IO;
using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<List<char>> grid = new List<List<char>>();

        while (reader.Peek() != -1)
        {
            grid.Add(reader.ReadLine().ToCharArray().ToList());
        }

        reader.Close();

        Tuple<int, int> startingCoords = null;
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                if (grid[i][j] == 'S') startingCoords = new Tuple<int, int>(i, j);
            }
        }

        if (startingCoords == null) throw new NullReferenceException("starting cooridinates shouldn't be null.");

        Console.WriteLine(GetFurthestPointInLoop(grid, startingCoords));
    }

    private static int GetFurthestPointInLoop(List<List<char>> grid, Tuple<int, int> startingCoords)
    {
        Tuple<int, int> currentCoords = startingCoords;

        List<Pipe> pipesInLoop = new List<Pipe>();
        pipesInLoop.Add(GetStartingPipe(grid, startingCoords));

        while (pipesInLoop.Last().ExitDirection != Direction.none)
        {
            switch (pipesInLoop.Last().ExitDirection)
            {
                case Direction.north:
                    currentCoords = new Tuple<int, int>(currentCoords.Item1 - 1, currentCoords.Item2);
                    break;
                case Direction.east:
                    currentCoords = new Tuple<int, int>(currentCoords.Item1, currentCoords.Item2 + 1);
                    break;
                case Direction.south:
                    currentCoords = new Tuple<int, int>(currentCoords.Item1 + 1, currentCoords.Item2);
                    break;
                case Direction.west:
                    currentCoords = new Tuple<int, int>(currentCoords.Item1, currentCoords.Item2 - 1);
                    break;
            }

            pipesInLoop.Add(new Pipe(grid[currentCoords.Item1][currentCoords.Item2], pipesInLoop.Last().ExitDirection));
        }

        return pipesInLoop.Count / 2;
    }

    private static Pipe GetStartingPipe(List<List<char>> grid, Tuple<int, int> startingCoords)
    {
        // Hard coded.
        return new Pipe('|', Direction.south);
    }
}
