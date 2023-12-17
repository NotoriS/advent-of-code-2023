using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
    {
        Thread t = new Thread(Run, 1024*1024*1024);
        t.Start();
    }

    public static void Run()
    {
        StreamReader reader = new StreamReader("../../../input.txt");
        List<List<int>> grid = new List<List<int>>();

        while (reader.Peek() != -1)
        {
            grid.Add(reader.ReadLine().ToCharArray().ToList().Select(e => int.Parse(e.ToString())).ToList());
        }

        Console.WriteLine(FindShortestPath(grid));
    }

    private static int FindShortestPath(List<List<int>> grid)
    {
        Dictionary<string, int> memoization = new Dictionary<string, int>();
        return FindShortestPathRec(grid, new Crucible(0, 0, Vector.Right), memoization);
    }

    private static int FindShortestPathRec(List<List<int>> grid, Crucible crucible, Dictionary<string, int> memoization)
    {
        if (crucible.Row < 0 || crucible.Row >= grid.Count || crucible.Col < 0 || crucible.Col >= grid[0].Count) return int.MaxValue;
        if (memoization.ContainsKey(crucible.ToString())) return memoization[crucible.ToString()];
        if (crucible.Row == grid.Count - 1 && crucible.Col == grid[0].Count) return grid[crucible.Row][crucible.Col];
        
        Crucible moveLeft = new Crucible(crucible);
        moveLeft.Move(Direction.Left);
        int moveLeftShortestPath = FindShortestPathRec(grid, moveLeft, memoization);

        Crucible moveRight = new Crucible(crucible);
        moveRight.Move(Direction.Right);
        int moveRightShortestPath = FindShortestPathRec(grid, moveRight, memoization);

        int moveStraightShortestPath = int.MaxValue;
        if (crucible.ConsecutiveStraightMoves < 3)
        {
            Crucible moveStraight = new Crucible(crucible);
            moveStraight.Move(Direction.Straight);
            moveStraightShortestPath = FindShortestPathRec(grid, moveStraight, memoization);
        }

        int shortestPath = grid[crucible.Row][crucible.Col] + Math.Min(Math.Min(moveLeftShortestPath, moveRightShortestPath), moveStraightShortestPath);

        memoization.Add(crucible.ToString(), shortestPath);
        return shortestPath;
    }
}
