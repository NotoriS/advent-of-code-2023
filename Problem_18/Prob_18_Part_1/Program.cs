using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        HashSet<Tuple<int, int>> result = new HashSet<Tuple<int, int>>();

        StreamReader reader = new StreamReader("../../../input.txt");
        Queue<Instruction> instructions = new Queue<Instruction>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            instructions.Enqueue(new Instruction(line.Split(' ')[0], int.Parse(line.Split(' ')[1])));
        }

        List<Tuple<int, int>> holeBorder = new List<Tuple<int, int>>();
        foreach (Instruction instruction in instructions)
        {
            instruction.Execute(holeBorder);
        }

        int rowOffset = holeBorder.Select(e => e.Item1).Min() * -1;
        if (rowOffset < 0) rowOffset = 0;

        int colOffset = holeBorder.Select(e => e.Item2).Min() * -1;
        if (colOffset < 0) colOffset = 0;

        int gridHeight = holeBorder.Select(e => e.Item1).Max() + rowOffset + 1;
        int gridWidth = holeBorder.Select(e => e.Item2).Max() + colOffset + 1;

        List<List<char>> grid = new List<List<char>>();
        for (int i = 0; i < gridHeight; i++)
        {
            List<char> row = new List<char>();
            for (int j = 0; j < gridWidth; j++)
            {
                row.Add('.');
            }
            grid.Add(row);
        }

        foreach (Tuple<int, int> coordinate in holeBorder)
        {
            grid[coordinate.Item1 + rowOffset][coordinate.Item2 + colOffset] = '#';
        }

        HashSet<Tuple<int, int>> noHoles = new HashSet<Tuple<int, int>>();
        FindNoHoleCoordinates(grid, noHoles);

        Console.WriteLine(grid.Count * grid[0].Count - noHoles.Count);
    }

    private static void FindNoHoleCoordinates(List<List<char>> grid, HashSet<Tuple<int, int>> noHoles)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            Expand(grid, noHoles, Tuple.Create(i, 0));
            Expand(grid, noHoles, Tuple.Create(i, grid[i].Count - 1));
        }
        for (int i = 0; i < grid[0].Count; i++)
        {
            Expand(grid, noHoles, Tuple.Create(0, i));
            Expand(grid, noHoles, Tuple.Create(grid.Count - 1, i));
        }
    }

    private static void Expand(List<List<char>> grid, HashSet<Tuple<int, int>> noHoles, Tuple<int, int> coordinate)
    {
        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        queue.Enqueue(coordinate);

        while (queue.Count > 0)
        {
            Tuple<int, int> next = queue.Dequeue();

            if (noHoles.Contains(next)) continue;
            if (CoordinateIsOutOfRange(grid, next)) continue;
            if (grid[next.Item1][next.Item2] == '#') continue;

            noHoles.Add(next);
            queue.Enqueue(Tuple.Create(next.Item1 + 1, next.Item2));
            queue.Enqueue(Tuple.Create(next.Item1 - 1, next.Item2));
            queue.Enqueue(Tuple.Create(next.Item1, next.Item2 + 1));
            queue.Enqueue(Tuple.Create(next.Item1, next.Item2 - 1));
        }
    }

    private static bool CoordinateIsOutOfRange(List<List<char>> grid, Tuple<int, int> coordinate)
    {
        return coordinate.Item1 < 0 || coordinate.Item1 >= grid.Count
            || coordinate.Item2 < 0 || coordinate.Item2 >= grid[0].Count;
    }
}
