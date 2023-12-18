using System.Collections.Generic;

public class Problem
{
    public static void Main(string[] args)
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
        Crucible first = new Crucible(0, 0, Vector.Right);
        PriorityQueue<Crucible, int> paths = new PriorityQueue<Crucible, int>();
        paths.Enqueue(first, first.HeatLost);

        Dictionary<string, int> memoization = new Dictionary<string, int>();

        int currentMinHeatLost = 0;
        int closestPointToEnd = 0;
        while (paths.Count > 0)
        {
            Crucible crucible = paths.Dequeue();

            string crucibleStr = crucible.ToString();
            if (memoization.ContainsKey(crucibleStr) && memoization[crucibleStr] < crucible.HeatLost) continue;
            memoization[crucibleStr] = crucible.HeatLost;

            if (crucible.HeatLost > currentMinHeatLost)
            {
                currentMinHeatLost = crucible.HeatLost;
                Console.WriteLine(currentMinHeatLost + "...");
            }

            if (crucible.Row + crucible.Col > closestPointToEnd)
            {
                closestPointToEnd = crucible.Row + crucible.Col;
                Console.WriteLine($"{crucible.Row},{crucible.Col}...");
            }

            if (crucible.IsAtEnd(grid)) 
            {
                crucible.PrintVisited(grid);
                return crucible.HeatLost; 
            }
            
            Crucible moveLeft = new Crucible(crucible);
            moveLeft.Move(Direction.Left, grid);
            if (moveLeft.HeatLost < int.MaxValue) paths.Enqueue(moveLeft, moveLeft.HeatLost);

            Crucible moveRight = new Crucible(crucible);
            moveRight.Move(Direction.Right, grid);
            if (moveRight.HeatLost < int.MaxValue) paths.Enqueue(moveRight, moveRight.HeatLost);

            Crucible moveStraight = new Crucible(crucible);
            moveStraight.Move(Direction.Straight, grid);
            if (moveStraight.HeatLost < int.MaxValue) paths.Enqueue(moveStraight, moveStraight.HeatLost);
        }

        return -1;
    }
}
